using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
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
            User? matchedUser = await _db.Users.FirstOrDefaultAsync(temp => temp.EmailAddress == emailAddress);

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


        //public async Task<User> AddSymbolToUserSymbolList(UserSymbol userSymbol)
        //{
        //    User? matchedUser = await _db.Users.Include(user => user.Symbols).FirstOrDefaultAsync(temp => temp.UserId == userSymbol.UserId);
        //    Symbol? matchedSymbol = await _db.Symbols.FirstOrDefaultAsync(temp => temp.SymbolId == userSymbol.SymbolId);



        //    if (matchedUser == null)
        //    {
        //        throw new Exception("no user finded with the given userId in AddSymbolToUserSymbolList-metod");
        //    }
        //    if (matchedSymbol == null)
        //    {
        //        throw new Exception("no symbol finded with the given userId in AddSymbolToUserSymbolList-metod");
        //    }

        //    // check for duplication of symbols in user-symbolList
        //    var x = matchedUser.Symbols.FirstOrDefault(temp => temp.SymbolId == matchedSymbol.SymbolId);
        //    if (x != null)
        //    {
        //        throw new Exception("this symbol is currently in userSymbolList");

        //    }


        //    matchedUser.Symbols.Add(matchedSymbol);
        //    await _db.SaveChangesAsync();
        //    return matchedUser;

        //}


    }
}
