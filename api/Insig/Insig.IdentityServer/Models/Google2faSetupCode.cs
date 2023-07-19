namespace Insig.IdentityServer.Models
{
    public class Google2faSetupCode
    {
        public string BarcodeImageUrl { get; set; }
        public string SetupCode { get; set; }
        public string Email { get; set; }
    }
}
