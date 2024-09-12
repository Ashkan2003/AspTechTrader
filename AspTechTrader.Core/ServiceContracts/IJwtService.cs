using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.ServiceContracts
{
    public interface IJwtService
    {
        AuthenticationResponseDTO CreateJwtToken(ApplicationUser applicationUser);
    }
}
