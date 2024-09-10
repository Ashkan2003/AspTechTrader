using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
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


        public async Task<User> AddNewUserWatchList(UserWatchListAddRequestDTO userWatchListAddRequest)
        {
            if (userWatchListAddRequest == null)
            {
                throw new ArgumentNullException(nameof(userWatchListAddRequest));
            }

            if (userWatchListAddRequest.UserId == Guid.Empty)
            {
                throw new ArithmeticException(nameof(userWatchListAddRequest.UserId));
            }

            User matchedUser = await _userWatchListsRepository.GetUserWithRelatedUserWatchListById(userWatchListAddRequest.UserId);

            return await _userWatchListsRepository.AddNewUserWatchList(userWatchListAddRequest);
        }
    }
}
