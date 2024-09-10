using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.Repositories
{
    public class SymbolsRepository : ISymbolsRepository
    {
        //private fields
        private readonly ApplicationDbContext _db;


        // constructure
        public SymbolsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<List<Symbol>> GetAllSymbools()
        {
            return await _db.Symbols
                .ToListAsync();
        }

        public async Task<List<Symbol>> GetAllSymbolsWithRelatedUserSymbolProperty()
        {
            return await _db.Symbols
                .Include(symbol => symbol.UserSymbolProperties)
                .ThenInclude(userSymbolPropertie => userSymbolPropertie.User)
                .ToListAsync();
        }

        public async Task<Symbol?> GetSymbolById(Guid SymbolId)
        {
            return await _db.Symbols.FirstOrDefaultAsync(temp => temp.SymbolId == SymbolId);
        }
    }
}
