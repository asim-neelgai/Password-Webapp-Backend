using Entities;
using Saas.Entities;
using Saas.Entities.Dtos;

namespace Saas.Repository.Services
{
    public interface IOrganizationService
    {
        Task<Result> AddOrganizationWithUser(Organization organization, OrganizationUser organizationUser);
        Task<bool> CheckIfUserIsPartOfOrganization(Guid userId, Guid organizationId);
        Task<Result> AddToOrganizationUserAndSharedSecret(
            Guid invitingUserId,
            string inviteeEmail,
            Guid organizationId,
            SharedSecret sharedSecret);
        Task<bool> CheckIfReceiverHasAccount(Guid organizationId, string email);
        Task<Result> AddToOrganizationUser(Guid invitingUserId, string inviteeEmail, Guid organizationId);
       Task<OrganizationUser?> GetUserOrganizationDetails(Guid organizationId, string email);
    }
}