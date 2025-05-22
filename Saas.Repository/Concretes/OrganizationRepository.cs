using Entities;
using Microsoft.EntityFrameworkCore;
using Saas.Data;
using Saas.Entities.enums;
using Saas.Repository.Interfaces;

namespace Saas.Repository.Concretes;
public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<List<Organization>> GetOrganizationsByUserIdAsync(Guid id)
    {
        return await GetAll().Include(x => x.OrganizationUsers).Where(u => u.OrganizationUsers.Any(x => x.UserId == id && x.Status== OrganizationUserStatus.Confirmed)).ToListAsync();
    }
    public async Task<bool> CheckOrganizationByUserId(Guid userId, Guid id)
    {
        return await GetAll().AnyAsync(s => s.CreatedBy == userId && s.Id == id);
    }
}
