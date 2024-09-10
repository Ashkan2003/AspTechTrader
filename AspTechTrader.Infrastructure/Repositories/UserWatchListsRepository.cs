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
                   .ThenInclude(u => u.Symbols)
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

        public async Task<UserWatchList?> AddNewSymbolToUserWatchList(AddSymbolToUserWatchListRequestDTO addSymbolToUserWatchListRequestDTO)
        {
            UserWatchList? matchedUserWatchList = await _db.UserWatchLists
                .Include(u => u.Symbols)
                .FirstOrDefaultAsync(temp => temp.UserWatchListId == addSymbolToUserWatchListRequestDTO.UserWatchListId);

            Symbol? matchedSymbol = await _db.Symbols.FirstOrDefaultAsync(temp => temp.SymbolId == addSymbolToUserWatchListRequestDTO.SymbolId);

            if (matchedUserWatchList == null)
            {
                throw new ArgumentNullException("there is no related userwatchList with the given userWatchListId");
            }

            if (matchedSymbol == null)
            {
                throw new ArgumentNullException("there is no related symbol with the given simbolId");
            }

            bool isSymbolInUserWatchList = matchedUserWatchList.Symbols.Contains(matchedSymbol);

            if (isSymbolInUserWatchList == true)
            {
                throw new Exception("this symbol allready exists in userWatchList");
            }

            matchedUserWatchList.Symbols.Add(matchedSymbol);

            await _db.SaveChangesAsync();

            return matchedUserWatchList;
        }
    }
}
