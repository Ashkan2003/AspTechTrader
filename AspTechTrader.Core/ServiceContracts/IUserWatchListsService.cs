using AspTechTrader.Core.Domain.Entities;

namespace AspTechTrader.Core.ServiceContracts
{
    public interface IUserWatchListsService
    {
        Task<User> GetUserWithRelatedUserWatchListById(Guid userId);

    }
}
