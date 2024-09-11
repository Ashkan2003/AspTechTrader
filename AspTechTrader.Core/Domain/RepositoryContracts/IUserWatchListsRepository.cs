using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.Domain.RepositoryContracts
{
    public interface IUserWatchListsRepository
    {
        Task<User?> GetUserWithRelatedUserWatchListById(Guid userId);

        Task<UserWatchList?> GetUserWatchListById(Guid userWatchListId);


        Task<User> AddNewUserWatchList(UserWatchListAddRequestDTO userWatchListAddRequest);

        Task<bool> DeleteUserWatchList(UserWatchListDeleteRequestDTO userWatchListDeleteRequestDTO);


        Task<UserWatchList?> AddNewSymbolToUserWatchList(AddSymbolToUserWatchListRequestDTO addSymbolToUserWatchListRequestDTO);

        Task<bool> RemoveSymbolFromUserWatchList(RemoveSymbolFromUserWatchListDTO removeSymbolFromUserWatchListDTO);

    }
}
