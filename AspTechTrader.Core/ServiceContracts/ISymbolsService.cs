using AspTechTrader.Core.Domain.Entities;

namespace AspTechTrader.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic 
    /// </summary>
    public interface ISymbolsService
    {
        /// <summary>
        /// get all of the symbols
        /// </summary>
        /// <returns>All symbols</returns>
        Task<List<Symbol>> GetAllSymbols();

        Task<List<Symbol>> GetAllSymbolsWithRelatedUserSymbolProperty();

        Task<Symbol?> GetSymbolById(Guid SymbolId);


    }
}
