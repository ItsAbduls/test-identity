using Google.Authenticator;
using Insig.IdentityServer.Models;
using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace Insig.IdentityServer.Infrastructure.Services
{
    public class GoogleAuthenticator2fa : IGoogleAuthenticator2fa
    {
        private readonly TwoFactorsSettings _settings;

        public GoogleAuthenticator2fa(IOptions<TwoFactorsSettings> settings)
        {
            _settings = settings.Value;
        }
        public Google2faSetupCode Generate2faSetupCode(string username)
        {
            var twoFactorAuthenticator = new TwoFactorAuthenticator();
            var accountSecretKey = GetSecret(username);
            try
            {
                var setupCode = twoFactorAuthenticator.GenerateSetupCode("Two Factor Demo App", username,
                Encoding.ASCII.GetBytes(accountSecretKey));
                return new Google2faSetupCode()
                {
                    BarcodeImageUrl = setupCode.QrCodeSetupImageUrl,
                    SetupCode = setupCode.ManualEntryKey,
                    Email = username
                };
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
               
        }

        public bool Verify2fa(string passCode, string username)
        {
            var twoFactorAuthenticator = new TwoFactorAuthenticator();
            var accountSecretKey = GetSecret(username);
            var result = twoFactorAuthenticator
                .ValidateTwoFactorPIN(accountSecretKey, passCode);
            return result;
        }
        private string GetSecret(string username)
        {
            var TwoFactorSecretCode = _settings.TwoFactorSecrets;
            var accountSecretKey = $"{TwoFactorSecretCode}-{username}";
            return accountSecretKey;
        }
    }
}
