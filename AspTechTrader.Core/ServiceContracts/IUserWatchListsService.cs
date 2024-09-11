using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.ServiceContracts
{
    public interface IUserWatchListsService
    {
        Task<User> GetUserWithRelatedUserWatchListById(Guid userId);

        Task<User> AddNewUserWatchList(UserWatchListAddRequestDTO userWatchListAddRequest);

        Task<bool> DeleteUserWatchList(UserWatchListDeleteRequestDTO userWatchListDeleteRequestDTO);


        Task<UserWatchList?> AddNewSymbolToUserWatchList(AddSymbolToUserWatchListRequestDTO addSymbolToUserWatchListRequestDTO);

        Task<bool> RemoveSymbolFromUserWatchList(RemoveSymbolFromUserWatchListDTO removeSymbolFromUserWatchListDTO);

    }
}
