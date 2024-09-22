using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {


        private readonly ApplicationDbContext _db;


        public UsersRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        // we make this optional becuz the User may not be exict
        public async Task<User?> GetUserById(Guid userId)
        {
            User? matchedUser = await _db.Users
                 .Include(user => user.UserSymbolProperties)
                 .ThenInclude(userSymbolProperty => userSymbolProperty.Symbol)
                 .Include(user => user.UserWatchLists)
                 .ThenInclude(userWatchList => userWatchList.Symbols)
                 .FirstOrDefaultAsync((temp) => temp.UserId == userId);

            return matchedUser;
        }

        public async Task<User?> GetUserByEmail(string emailAddress)
        {
            User? matchedUser = await _db.Users
                .Include(user => user.UserSymbolProperties)
                .ThenInclude(userSymbolProperty => userSymbolProperty.Symbol)
                .Include(user => user.UserWatchLists)
                .ThenInclude(userWatchList => userWatchList.Symbols)
                .FirstOrDefaultAsync(temp => temp.EmailAddress == emailAddress);

            return matchedUser;
        }


        public async Task<User> AddUser(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            User? matchedUser = await _db.Users.FirstOrDefaultAsync((temp) => temp.UserId == user.UserId);


            matchedUser.UserName = user.UserName;
            matchedUser.EmailAddress = user.EmailAddress;
            matchedUser.UserProperty = user.UserProperty;

            int countUpdated = await _db.SaveChangesAsync();

            return user;

        }

        public async Task<bool> DeleteUserById(Guid userId)
        {
            _db.RemoveRange(_db.Users.Where(temp => temp.UserId == userId));
            int rowDeleted = await _db.SaveChangesAsync();

            return rowDeleted > 0;

        }

        public async Task<bool> UpdateUserProperty(UserPropertyUpdateRequestDTO userPropertyUpdateRequestDTO)
        {
            User? matchedUser = await _db.Users.FirstOrDefaultAsync(temp => temp.UserId == userPropertyUpdateRequestDTO.UserId);

            matchedUser.UserProperty = userPropertyUpdateRequestDTO.UserProperty;

            int count = await _db.SaveChangesAsync();

            return count > 0 ? true : false;
        }



    }
}
