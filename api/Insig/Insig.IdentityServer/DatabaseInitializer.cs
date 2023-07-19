using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Insig.IdentityServer
{
    public class DatabaseInitializer
    {
        public static void Init(IServiceProvider provider, bool useInMemoryStores, string clientUri)
        {
            if (!useInMemoryStores)
            {
                provider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                provider.GetRequiredService<ConfigurationDbContext>().Database.Migrate();
            }
            InitializeIdentityServer(provider, clientUri);

            //var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            //var chsakell = userManager.FindByNameAsync("chsakell").Result;
            //if (chsakell == null)
            //{
            //    chsakell = new IdentityUser
            //    {
            //        UserName = "chsakell"
            //    };
            //    var result = userManager.CreateAsync(chsakell, "$AspNetIdentity10$").Result;
            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }

            //    chsakell = userManager.FindByNameAsync("chsakell").Result;

            //    result = userManager.AddClaimsAsync(chsakell, new Claim[]{
            //        new Claim(JwtClaimTypes.Name, "Chris Sakellarios"),
            //        new Claim(JwtClaimTypes.GivenName, "Christos"),
            //        new Claim(JwtClaimTypes.FamilyName, "Sakellarios"),
            //        new Claim(JwtClaimTypes.Email, "chsakellsblog@blog.com"),
            //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
            //        new Claim(JwtClaimTypes.WebSite, "https://chsakell.com"),
            //        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'localhost 10', 'postal_code': 11146, 'country': 'Greece' }",
            //            IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
            //    }).Result;

            //    if (!result.Succeeded)
            //    {
            //        throw new Exception(result.Errors.First().Description);
            //    }
            //    Console.WriteLine("chsakell created");
            //}
            //else
            //{
            //    Console.WriteLine("chsakell already exists");
            //}
        }

        private static void InitializeIdentityServer(IServiceProvider provider, string clientUri)
        {
            var context = provider.GetRequiredService<ConfigurationDbContext>();


            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.GetApiScopes())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients(clientUri))
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
