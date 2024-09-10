using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.ServiceContracts;

namespace AspTechTrader.Core.Services
{
    public class SymbolsService : ISymbolsService
    {
        private readonly ISymbolsRepository _symbolsRepository;

        public SymbolsService(ISymbolsRepository symbolsRepository)
        {
            _symbolsRepository = symbolsRepository;
        }

        public async Task<List<Symbol>> GetAllSymbols()
        {
            List<Symbol> symbols = await _symbolsRepository.GetAllSymbools();

            return symbols;
        }

        public async Task<List<Symbol>> GetAllSymbolsWithRelatedUserSymbolProperty()
        {
            List<Symbol> symbolsWithRelatedUserSymbolProperty = await _symbolsRepository.GetAllSymbolsWithRelatedUserSymbolProperty();

            return symbolsWithRelatedUserSymbolProperty;
        }

        public async Task<Symbol?> GetSymbolById(Guid SymbolId)
        {
            if (SymbolId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(SymbolId));
            }

            Symbol? matchedSymbol = await _symbolsRepository.GetSymbolById(SymbolId);

            return matchedSymbol;
        }
    }
}
