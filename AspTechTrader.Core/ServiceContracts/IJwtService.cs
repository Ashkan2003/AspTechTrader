using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.DTO;
using System.Security.Claims;

namespace AspTechTrader.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponseDTO CreateJwtToken(ApplicationUser applicationUser);

        ClaimsPrincipal? GetPrincipalFormJwtToken(string? token);
    }
}
