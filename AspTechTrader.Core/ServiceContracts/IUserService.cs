using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.DTO;

namespace AspTechTrader.Core.ServiceContracts
{
    /// <summary>
    /// Represents business logic (retrieve) for manipulating user entity
    /// </summary>
    public interface IUserService
    {
        Task<User?> GetUserById(Guid? userId);

        Task<User> AddUser(UserAddRequestDTO? userAddRequest);


        Task<User> UpdateUser(UserUpdateRequestDTO userUpdateRequest);

        Task<bool> DeleteUserById(Guid? userId);

        //Task<User> AddSymbolToUserSymbolList(UserSymbol userSymbol);

    }
}
