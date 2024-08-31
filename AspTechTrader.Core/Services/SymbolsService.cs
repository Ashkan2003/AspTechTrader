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
    }
}
