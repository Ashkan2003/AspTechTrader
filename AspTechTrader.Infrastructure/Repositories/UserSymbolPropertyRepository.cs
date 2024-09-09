using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.DTO;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.Repositories
{
    public class UserSymbolPropertyRepository : IUserSymbolPropertyRepository
    {

        private readonly ApplicationDbContext _db;


        public UserSymbolPropertyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> AddNewBoughtSymbol(UserBoughtSymbolAddRequestDTO userBoughtSymbolAddRequest)
        {

            if (userBoughtSymbolAddRequest == null)
            {
                throw new ArgumentNullException(nameof(userBoughtSymbolAddRequest));
            }

            User? matchedUser = await _db.Users
                .Include(user => user.UserSymbolProperties)
                .ThenInclude(userSymbolProperty => userSymbolProperty.Symbol)
                .FirstOrDefaultAsync(temp => temp.UserId == userBoughtSymbolAddRequest.UserId);

            if (matchedUser == null)
            {
                throw new Exception("there is no user with the given userId");
            }


            UserSymbolProperty? matchedSymbol = matchedUser.UserSymbolProperties.FirstOrDefault(temp => temp.SymbolId == userBoughtSymbolAddRequest.SymbolId);

            if (matchedSymbol != null)
            {
                throw new Exception("user already have this symbol");
            }

            _db.UserSymbolProperties.Add(new UserSymbolProperty()
            {
                UserSymbolPropertyId = Guid.NewGuid(),

                SymbolPrice = userBoughtSymbolAddRequest.SymbolPrice,
                SymbolQuantity = userBoughtSymbolAddRequest.SymbolQuantity,
                UserId = userBoughtSymbolAddRequest.UserId,
                SymbolId = userBoughtSymbolAddRequest.SymbolId,
            });



            var count = await _db.SaveChangesAsync();


            return matchedUser;

        }







    }
}
