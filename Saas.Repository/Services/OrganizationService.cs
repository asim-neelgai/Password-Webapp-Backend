using Entities;
using Saas.Data;
using Saas.Entities;
using Saas.Entities.Dtos;
using Saas.Entities.enums;
using Saas.Repository.Interfaces;

namespace Saas.Repository.Services
{
    public class OrganizationService(
        IOrganizationRepository _organizationRepository,
        IOrganizationUserRepository _organizationUserRepository,
        ISharedSecretRepository _sharedSecretRepository,
        ApplicationDbContext _dbContext
    ) : IOrganizationService
    {
        public async Task<Result> AddOrganizationWithUser(Organization organization, OrganizationUser organizationUser)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                await _organizationRepository.AddAsync(organization);
                organizationUser.OrganizationId = organization.Id;
                await _organizationUserRepository.AddAsync(organizationUser);
                await transaction.CommitAsync();
            }

            return new Result(true, "Organization added successfully");
        }
        public async Task<bool> CheckIfUserIsPartOfOrganization(Guid userId, Guid organizationId)
        {
            return await _organizationUserRepository.CheckOrganizationUserByIdAsync(userId, organizationId);
        }
        public async Task<bool> CheckIfReceiverHasAccount(Guid organizationId, string email)
        {
            return await _organizationUserRepository.CheckOrganizationUserByEmailAsync(organizationId, email);
        }
        public async Task<OrganizationUser?> GetUserOrganizationDetails(Guid organizationId, string email)
        {
            return await _organizationUserRepository.GetOrganizationUserByOrgIdEmailAsync(organizationId, email);
        }
        public async Task<Result> AddToOrganizationUserAndSharedSecret(
            Guid invitingUserId,
            string inviteeEmail,
            Guid organizationId,
            SharedSecret sharedSecret)
        {
            var organizationUser = new OrganizationUser
            {
                CreatedBy = invitingUserId,
                UpdatedBy = invitingUserId,
                InvitingUserId = invitingUserId,
                OrganizationId = organizationId,
                Email = inviteeEmail,
                Status = OrganizationUserStatus.Invited
            };
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                await _organizationUserRepository.AddAsync(organizationUser);
                await _sharedSecretRepository.AddAsync(sharedSecret);
                await transaction.CommitAsync();
            }

            return new Result(true, "OrganizationUser and Shared secret added successfully");
        }
        public async Task<Result> AddToOrganizationUser(Guid invitingUserId, string inviteeEmail, Guid organizationId)
        {
            var organizationUser = new OrganizationUser
            {
                CreatedBy = invitingUserId,
                UpdatedBy = invitingUserId,
                InvitingUserId = invitingUserId,
                OrganizationId = organizationId,
                Email = inviteeEmail,
                Status = OrganizationUserStatus.Invited
            };
            await _organizationUserRepository.AddAsync(organizationUser);

            return new Result(true, "OrganizationUser added successfully");
        }
    }
}