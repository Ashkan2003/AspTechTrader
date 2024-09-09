using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
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
    }
}
