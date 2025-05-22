using Entities;
using Microsoft.EntityFrameworkCore;
using Saas.Repository.Interfaces;
using Saas.Data;
using Saas.Entities;

namespace Saas.Repository.Concretes;
public class SharedSecretRepository : Repository<SharedSecret>, ISharedSecretRepository
{
    public SharedSecretRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<List<SharedSecret>> GetSharedSecretsByUserIdAsync(Guid id)
    {
        return await GetAll().Where(u => u.CreatedBy == id).ToListAsync();
    }
   public async Task<bool> CheckIfAlreadyShared(Guid sharedToId, Guid secretId)
    {
        return await GetAll().AnyAsync(s => s.SharedTo == sharedToId && s.SecretId == secretId);
    }
   
}