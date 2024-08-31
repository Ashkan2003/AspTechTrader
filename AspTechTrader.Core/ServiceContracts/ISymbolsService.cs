﻿using AspTechTrader.Core.Domain.Entities;

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
    }
}
