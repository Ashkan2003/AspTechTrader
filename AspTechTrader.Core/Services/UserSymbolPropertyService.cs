using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Core.ServiceContracts;

namespace AspTechTrader.Core.Services
{
    public class UserSymbolPropertyService : IUserSymbolPropertyService
    {


        private readonly IUserSymbolPropertyRepository _userSymbolPropertyRepository;


        public UserSymbolPropertyService(IUserSymbolPropertyRepository userSymbolPropertyRepository)
        {
            _userSymbolPropertyRepository = userSymbolPropertyRepository;
        }


        public async Task<User> AddNewBoughtSymbol(UserBoughtSymbolAddRequestDTO userBoughtSymbolAddRequest)
        {
            User user = await _userSymbolPropertyRepository.AddNewBoughtSymbol(userBoughtSymbolAddRequest);
            return user;
        }
    }
}
