using AspTechTrader.Core.Domain.Entities;

namespace AspTechTrader.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Represents data access logic for managing user-entity
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// get the user from db by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>uniq user</returns>
        Task<User?> GetUserById(Guid userId);

        /// <summary>
        /// add user to bd
        /// </summary>
        /// <param name="user"></param>
        /// <returns>the added user</returns>
        Task<User> AddUser(User user);

        /// <summary>
        /// update the given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Updated user</returns>
        Task<User> UpdateUser(User user);

        /// <summary>
        /// delete user by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true if deleted successfully</returns>
        Task<bool> DeleteUserById(Guid userId);

        Task<User> AddSymbolToUserSymbolList(UserSymbol userSymbol);
    }
}
