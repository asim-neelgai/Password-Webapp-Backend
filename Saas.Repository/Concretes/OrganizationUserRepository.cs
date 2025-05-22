using Entities;
using Microsoft.EntityFrameworkCore;
using Saas.Data;
using Saas.Repository.Interfaces;

namespace Saas.Repository.Concretes;
public class OrganizationUserRepository : Repository<OrganizationUser>, IOrganizationUserRepository
{
    public OrganizationUserRepository(ApplicationDbContext context) : base(context)
    {
    }
    public async Task<List<OrganizationUser>> GetOrganizationUserByUserIdAsync(Guid id)
    {
        return await GetAll().Include(x => x.Organization).Where(u => u.UserId == id).ToListAsync();
    }
    public async Task<OrganizationUser?> GetOrganizationUserByOrgIdUserIdAsync(Guid organizationId, Guid creatorId)
    {
        return await GetAll().Include(x => x.Organization)
            .FirstOrDefaultAsync(u => u.CreatedBy == creatorId && u.UserId == creatorId && u.OrganizationId == organizationId);
    }
    public async Task<List<OrganizationUser>> GetAllOrganizationUserByOrgIdUserIdAsync(Guid organizationId, Guid creatorId)
    {
        return await GetAll().Include(x => x.Organization)
            .Where(u => u.CreatedBy == creatorId && u.OrganizationId == organizationId).ToListAsync();
    }
    public async Task<OrganizationUser?> GetOrganizationUserByOrgIdEmailAsync(Guid organizationId, string email)
    {
        return await GetAll().Include(x => x.Organization).Include(x => x.Creator).Include(x => x.User)
            .FirstOrDefaultAsync(u => u.Email == email && u.OrganizationId == organizationId);
    }

    public async Task<bool> CheckOrganizationUserByEmailAsync(Guid organizationId, string email)
    {
        return await GetAll().AnyAsync(u => u.OrganizationId == organizationId && u.Email == email);
    }
    public async Task<bool> CheckOrganizationUserByIdAsync(Guid userId, Guid organizationId)
    {
        return await GetAll().AnyAsync(u => u.OrganizationId == organizationId && u.UserId == userId);
    }
    public async Task<bool> CheckOrganizationCreatorAsync(Guid userId, Guid id)
    {
        return await GetAll().AnyAsync(u => u.CreatedBy == userId && u.Id == id);
    }
}
