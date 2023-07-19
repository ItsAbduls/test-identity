using Insig.IdentityServer.Models;

namespace Insig.IdentityServer.Infrastructure.Services
{
    public interface IGoogleAuthenticator2fa
    {
        public Google2faSetupCode Generate2faSetupCode(string username);
        public bool Verify2fa(string passCode, string username);
    }
}
