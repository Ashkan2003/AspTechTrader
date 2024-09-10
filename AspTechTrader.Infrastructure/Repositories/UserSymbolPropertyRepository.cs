using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Infrastructure.AppDbContext;

namespace AspTechTrader.Infrastructure.Repositories
{
    public class UserSymbolPropertyRepository : IUserSymbolPropertyRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly IUsersRepository _usersRepository;

        public UserSymbolPropertyRepository(ApplicationDbContext db, IUsersRepository usersRepository)
        {
            _db = db;
            _usersRepository = usersRepository;
        }

        public async Task<User> AddNewBoughtSymbol(UserSymbolProperty userSymbolProperty)
        {

            User? matchedUser = await _usersRepository.GetUserById(userSymbolProperty.UserId.Value);

            matchedUser.UserSymbolProperties.Add(userSymbolProperty);

            var count = await _db.SaveChangesAsync();


            return matchedUser;

        }







    }
}
