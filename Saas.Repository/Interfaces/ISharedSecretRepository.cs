using Entities;
using Saas.Entities;

namespace Saas.Repository.Interfaces
{
    public interface ISharedSecretRepository : IRepository<SharedSecret>
    {
        Task<List<SharedSecret>> GetSharedSecretsByUserIdAsync(Guid id);
        Task<bool> CheckIfAlreadyShared(Guid sharedToId, Guid secretId);
    }
}