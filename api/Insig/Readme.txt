git repo = https://github.com/pklejnowski?tab=repositories
sometime if migration did't work read this link
https://stackoverflow.com/questions/61449890/ef-migration-could-not-load-assembly-ensure-it-is-referenced-by-the-startup-pr

2. if external login gives error NoAllowed then refer to this link
https://stackoverflow.com/questions/41598437/signinmanagerexternalloginsigninasync-returns-isnotallowed-for-social-login

3. to create projects of external login
 - https://console.cloud.google.com/
 - azure portal
 -
2. Migration commands 
 -  add-migration InitialMigration -Context AppIdentityDbContext
 -  add-migration InitialMigration -Context InsigContext
 -  update-database -Context AppIdentityDbContext
 -  update-database -Context InsigContext
3. how to install service bus explorer
 - http://adminwebsite-api-staging.azurewebsites.net/web/login

 https://endjin.com/blog/2022/03/adding-authentication-and-authorisation-to-aspnet-core-web-applications
 https://programmingcsharp.com/implement-identity-on-existing-asp-project/