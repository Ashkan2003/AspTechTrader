﻿using AspTechTrader.Core.Domain.Entities;

namespace AspTechTrader.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing symbol-entity
    /// </summary>
    public interface ISymbolsRepository
    {
        /// <summary>
        /// get all symbols from the db
        /// </summary>
        /// <returns>All symbols from db</returns>
        Task<List<Symbol>> GetAllSymbools();
    }
}