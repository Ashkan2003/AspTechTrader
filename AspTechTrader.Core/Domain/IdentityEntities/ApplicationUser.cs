using Microsoft.AspNetCore.Identity;

namespace AspTechTrader.Core.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? PersonName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDateTime { get; set; }    
    }
}
