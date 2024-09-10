using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
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







    }
}
