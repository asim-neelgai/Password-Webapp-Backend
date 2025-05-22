using Entities;

namespace Saas.Repository.Interfaces
{
    public interface IOrganizationUserRepository : IRepository<OrganizationUser>
    {
        Task<List<OrganizationUser>> GetOrganizationUserByUserIdAsync(Guid id);
        Task<bool> CheckOrganizationUserByEmailAsync(Guid organizationId, string email);
        Task<OrganizationUser?> GetOrganizationUserByOrgIdUserIdAsync(Guid organizationId, Guid creatorId);
        Task<bool> CheckOrganizationCreatorAsync(Guid userId, Guid id);
        Task<bool> CheckOrganizationUserByIdAsync(Guid userId, Guid organizationId);
        Task<OrganizationUser?> GetOrganizationUserByOrgIdEmailAsync(Guid organizationId, string email);
        Task<List<OrganizationUser>> GetAllOrganizationUserByOrgIdUserIdAsync(Guid organizationId, Guid creatorId);
    }
}