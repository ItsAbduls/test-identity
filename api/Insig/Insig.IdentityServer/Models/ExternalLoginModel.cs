using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Insig.IdentityServer.Models
{
    public class ExternalLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ClaimsPrincipal Principal { get; set; }
        public string RedirectUrl { get; set; } = "http://localhost:4200/2fa";
    }
}
