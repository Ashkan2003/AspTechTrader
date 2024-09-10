using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.Repositories
{
    public class UserWatchListsRepository : IUserWatchListsRepository
    {
        private readonly ApplicationDbContext _db;

        public UserWatchListsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserWithRelatedUserWatchListById(Guid userId)
        {
            User? MatchedUser = await _db.Users
                   .Include(u => u.UserWatchLists)
                   .FirstOrDefaultAsync(u => u.UserId == userId);

            return MatchedUser;
        }

        public async Task<User> AddNewUserWatchList(UserWatchListAddRequestDTO userWatchListAddRequest)
        {
            User matchedUser = await GetUserWithRelatedUserWatchListById(userWatchListAddRequest.UserId);

            if (matchedUser == null)
            {
                throw new ArgumentException("no user founded with the given user id");
            };

            matchedUser.UserWatchLists.Add(new UserWatchList()
            {
                UserWatchListId = Guid.NewGuid(),
                userWatchListName = userWatchListAddRequest.userWatchListName,

                UserId = matchedUser.UserId
            });

            await _db.SaveChangesAsync();

            return matchedUser;
        }
    }
}
