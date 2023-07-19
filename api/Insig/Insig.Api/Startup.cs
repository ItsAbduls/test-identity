using System;
using System.Net;
using System.Security.Claims;
using Autofac;
using IdentityServer4.AccessTokenValidation;
using Insig.Api.Infrastructure;
using Insig.Api.timeServiceSignalR;
using Insig.Common.Auth;
using Insig.Common.Configuration;
using Insig.Infrastructure.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using StackExchange.Redis;

namespace Insig.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHttpContextAccessor();

            ConfigureAuth(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var serilog = new LoggerConfiguration()
                  .MinimumLevel.Verbose()
                  .Enrich.FromLogContext()
                  .WriteTo.File(@"api_log.txt");

            loggerFactory.WithFilter(new FilterLoggerSettings
            {
                {"Microsoft", LogLevel.Warning},
                {"System", LogLevel.Warning}
            }).AddSerilog(serilog.CreateLogger());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseApiSecurityHttpHeaders();
            app.UseBlockingContentSecurityPolicyHttpHeader();
            app.RemoveServerHeader();
            app.UseNoCacheHttpHeaders();
            app.UseStrictTransportSecurityHttpHeader(env);
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(b => b.WithOrigins(Configuration["AppConfig:ClientUrl"], "https://localhost:5001", "http://localhost:4300", "https://locahost:5005").AllowAnyHeader().AllowAnyMethod()
            .AllowCredentials() // AllowCredentials solved origion problem to 5001 
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(configure =>
            {
                configure.MapHub<ViewHub>("/hubs/view");
                configure.MapHub<ColorHub>("/hubs/color");
                configure.MapHub<VoteHub>("/hubs/vote");
                configure.MapHub<TimeHub>("/hubs/timehub");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public virtual void ConfigureAuth(IServiceCollection services)
        {
            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["AppConfig:IdentityUrl"];
                    options.RequireHttpsMetadata = true;
                    options.ApiName = Instances.InsigApi;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ApiReader, policy => policy.RequireClaim("scope", Scopes.InsigApi));
                options.AddPolicy(Policies.Consumer, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Consumer));
                options.AddPolicy(Policies.Color, policy => policy.RequireClaim(ClaimTypes.Role, Roles.ColorChange));
            });

            services.AddMvcCore(options =>
            {
                options.Filters.Add(new AuthorizeFilter(Policies.ApiReader));
            });
            var redisCon = "Redis-demo-app1.redis.cache.windows.net:6380,password=mcvavZPTz6YxFQbqqanYfzGW4YsZBHpeSAzCaFAts7w=,ssl=True,abortConnect=False";


            services.AddSignalR().AddMessagePackProtocol()//Message pack support
                                    .AddMessagePackProtocol()
                                    //Using redis as the backplane to support scale out
                                    .AddStackExchangeRedis(o =>
                                    {
                                        o.ConnectionFactory = async writer =>
                                        {
                                            var config = new ConfigurationOptions
                                            {
                                                AbortOnConnectFail = false,
                                                ChannelPrefix = "__signalr_",
                                            };
                                            config.DefaultDatabase = 10;
                                            var connection = await ConnectionMultiplexer.ConnectAsync(redisCon, writer);
                                            connection.ConnectionFailed += (_, e) =>
                                            {
                                                Console.WriteLine("Connection to Redis failed.");
                                            };

                                            if (connection.IsConnected)
                                            {
                                                Console.WriteLine("connected to Redis.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Did not connect to Redis");
                                            }

                                            return connection;
                                        };
                                    });
            services.AddHostedService<TimeService>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DefaultModule(Configuration.GetConnectionString("Insig")));
        }
    }
}
