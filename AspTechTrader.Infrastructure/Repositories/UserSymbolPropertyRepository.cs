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

        public async Task<User> AddNewBoughtSymbol(UserSymbolProperty userSymbolProperty)
        {

            User? matchedUser = await _db.Users
                .Include(u => u.UserSymbolProperties)
                .ThenInclude(u => u.Symbol)
                .FirstOrDefaultAsync(temp => temp.UserId == userSymbolProperty.UserId);

            matchedUser.UserSymbolProperties.Add(userSymbolProperty);

            var count = await _db.SaveChangesAsync();


            return matchedUser;

        }

        public async Task<bool> DeleteBoughtSymbol(Guid userSymbolPropertyId)
        {
            UserSymbolProperty? userSymbolProperty = await _db.UserSymbolProperties.FirstOrDefaultAsync(temp => temp.UserSymbolPropertyId == userSymbolPropertyId);

            _db.UserSymbolProperties.Remove(userSymbolProperty);

            var count = await _db.SaveChangesAsync();

            return count > 0 ? true : false;

        }

        public async Task<bool> UpdateUserSymbolProperty(UserSymbolPropertyUpdateRequestDTO userSymbolPropertyUpdateDTO)
        {

            UserSymbolProperty? matchedUserSymbolProperty = await _db.UserSymbolProperties.FirstOrDefaultAsync(temp => temp.UserSymbolPropertyId == userSymbolPropertyUpdateDTO.UserSymbolPropertyId);

            matchedUserSymbolProperty.SymbolPrice = userSymbolPropertyUpdateDTO.SymbolPrice;
            matchedUserSymbolProperty.SymbolQuantity = userSymbolPropertyUpdateDTO.SymbolQuantity;


            var count = await _db.SaveChangesAsync();

            return count > 0 ? true : false;


        }
    }
}
