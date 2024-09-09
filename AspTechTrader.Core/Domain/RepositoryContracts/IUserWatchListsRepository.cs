using AspTechTrader.Core.Domain.Entities;

namespace AspTechTrader.Core.Domain.RepositoryContracts
{
    public interface IUserWatchListsRepository
    {
        Task<User> GetUserWithRelatedUserWatchListById(Guid userId);
    }
}
