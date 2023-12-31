﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Insig.IdentityServer.Infrastructure.Extensions
{
    public static class ExternalProvidersRegistrations
    {
        public static void ConfigureExternalProviders(this IServiceCollection services, IConfiguration configuration)
        {
            // userful links 
            // https://peteskelly.com/authenticating-linkedin-users-using-asp-net-contrib-oauth-providers/
            // https://damienbod.com/2017/07/11/adding-an-external-microsoft-login-to-identityserver4/
            // https://chsakell.com/2019/07/28/asp-net-core-identity-series-external-provider-authentication-registration-strategy/#google
            // https://github.com/CodeMazeBlog/identity-aspnetcore.git
            // Google

            //dotnet user-secrets set "Authentication:Google:ClientId" ""
            //dotnet user-secrets set "Authentication:Google:ClientSecret" ""

            if (configuration["Authentication:Google:ClientId"] != null)
            {
                services.AddAuthentication().AddGoogle(o =>
                {
                    o.ClientId = configuration["Authentication:Google:ClientId"];
                    o.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                });
            }

            // Facebook

            // dotnet user-secrets set Authentication:Facebook:AppId ""
            // dotnet user-secrets set Authentication:Facebook:AppSecret ""

            if (configuration["Authentication:Facebook:AppId"] != null)
            {
                services.AddAuthentication().AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                });
            }

            // Twitter

            // dotnet user-secrets set Authentication:Twitter:ConsumerAPIKey ""
            // dotnet user-secrets set Authentication:Twitter:ConsumerAPISecret ""

            if (configuration["Authentication:Twitter:ConsumerAPIKey"] != null)
            {
                services.AddAuthentication().AddTwitter(twitterOptions =>
                {
                    twitterOptions.ConsumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];
                    twitterOptions.ConsumerSecret = configuration["Authentication:Twitter:ConsumerAPISecret"];
                    twitterOptions.RetrieveUserDetails = true;
                });
            }

            // Microsoft

            // dotnet user-secrets set Authentication:Microsoft:ClientId ""
            // dotnet user-secrets set Authentication:Microsoft:ClientSecret ""
            // https://account.live.com/consent/Manage revoke permission to this app
            if (configuration["Authentication:Microsoft:ClientId"] != null)
            {
                services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = configuration["Authentication:Microsoft:ClientId"];
                    microsoftOptions.ClientSecret = configuration["Authentication:Microsoft:ClientSecret"];
                });
            }

            // GitHub

            // dotnet user-secrets set Authentication:GitHub:ClientId ""
            // dotnet user-secrets set Authentication:GitHub:ClientSecret ""

            if (configuration["Authentication:GitHub:ClientId"] != null)
            {
                services.AddAuthentication().AddGitHub(gitHubOptions =>
                {
                    gitHubOptions.ClientId = configuration["Authentication:GitHub:ClientId"];
                    gitHubOptions.ClientSecret = configuration["Authentication:GitHub:ClientSecret"];
                });
            }

            // LinkedIn

            // dotnet user-secrets set Authentication:LinkedIn:ClientId ""
            // dotnet user-secrets set Authentication:LinkedIn:ClientSecret ""

            if (configuration["Authentication:LinkedIn:ClientId"] != null)
            {
                services.AddAuthentication().AddLinkedIn(linkedInOptions =>
                {
                    linkedInOptions.ClientId = configuration["Authentication:LinkedIn:ClientId"];
                    linkedInOptions.ClientSecret = configuration["Authentication:LinkedIn:ClientSecret"];
                    //linkedInOptions.CallbackPath = "/signin-linkedin";
                });
            }

            // DropBox

            // dotnet user-secrets set Authentication:DropBox:ClientKey ""
            // dotnet user-secrets set Authentication:DropBox:ClientSecret ""

            if (configuration["Authentication:DropBox:ClientKey"] != null)
            {
                services.AddAuthentication().AddDropbox(dropBoxOptions =>
                {
                    dropBoxOptions.ClientId = configuration["Authentication:DropBox:ClientKey"];
                    dropBoxOptions.ClientSecret = configuration["Authentication:DropBox:ClientSecret"];
                    //dropBoxOptions.CallbackPath = "/signin-dropbox";
                });
            }
            
        }
    }
}
