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
                .FirstOrDefaultAsync(temp => temp.UserId == userBoughtSymbolAddRequest.UserId);

            //Symbol? matchedSymbol = await _db.Symbols
            //    //.Include(symbol => symbol.UserSymbolProperties)
            //    .FirstOrDefaultAsync(temp => temp.SymbolId == userBoughtSymbolAddRequest.SymbolId);

            if (matchedUser == null)
            {
                throw new Exception("there is no user with the given userId");
            }

            //if (matchedSymbol == null)
            //{
            //    throw new Exception("there is no symbol with the given symbolId");
            //}
            // _db.Entry(matchedUser).State = EntityState.Modified; // added row

            UserSymbolProperty userSymbolPropertytoAdd = new UserSymbolProperty()
            {
                UserSymbolPropertyId = Guid.NewGuid(),

                SymbolPrice = userBoughtSymbolAddRequest.SymbolPrice,
                SymbolQuantity = userBoughtSymbolAddRequest.SymbolQuantity,
                UserId = userBoughtSymbolAddRequest.UserId
            };



            //matchedUser.UserSymbolProperties.Add(new UserSymbolProperty()
            //{
            //    UserSymbolPropertyId = Guid.NewGuid(),

            //    SymbolPrice = userBoughtSymbolAddRequest.SymbolPrice,
            //    SymbolQuantity = userBoughtSymbolAddRequest.SymbolQuantity,
            //});
            matchedUser.UserSymbolProperties.Append(userSymbolPropertytoAdd);
          //  _db.Entry(matchedUser).CurrentValues.SetValues(matchedUser.UserSymbolProperties);

            var count = await _db.SaveChangesAsync();


            return matchedUser;

        }







    }
}
