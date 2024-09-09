
using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.Domain.RepositoryContracts
{
    public interface IUserSymbolPropertyRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userSymbolProperty"></param>
        /// <returns></returns>
        public Task<User> AddNewBoughtSymbol(UserBoughtSymbolAddRequestDTO userBoughtSymbolAddRequest);
    }
}
