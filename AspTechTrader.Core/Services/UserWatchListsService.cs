using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.ServiceContracts;

namespace AspTechTrader.Core.Services
{
    public class UserWatchListsService : IUserWatchListsService
    {
        private readonly IUserWatchListsRepository _userWatchListsRepository;

        public UserWatchListsService(IUserWatchListsRepository userWatchListsRepository)
        {
            _userWatchListsRepository = userWatchListsRepository;
        }

        public async Task<User> GetUserWithRelatedUserWatchListById(Guid userId)
        {

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            User? UserWithRelatedUserWatchList = await _userWatchListsRepository.GetUserWithRelatedUserWatchListById(userId);

            if (UserWithRelatedUserWatchList == null)
            {
                throw new Exception("no user founded with the given id");

            }

            return UserWithRelatedUserWatchList;
        }
    }
}
