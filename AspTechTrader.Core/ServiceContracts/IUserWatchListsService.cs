using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.ServiceContracts
{
    public interface IUserWatchListsService
    {
        Task<User> GetUserWithRelatedUserWatchListById(Guid userId);

        Task<User> AddNewUserWatchList(UserWatchListAddRequestDTO userWatchListAddRequest);

        Task<UserWatchList?> AddNewSymbolToUserWatchList(AddSymbolToUserWatchListRequestDTO addSymbolToUserWatchListRequestDTO);

    }
}
