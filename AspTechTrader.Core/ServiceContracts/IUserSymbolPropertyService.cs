using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.ServiceContracts
{
    public interface IUserSymbolPropertyService
    {
        Task<User> AddNewBoughtSymbol(UserBoughtSymbolAddRequestDTO userBoughtSymbolAddRequest);


    }
}
