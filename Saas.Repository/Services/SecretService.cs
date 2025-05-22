using System.Linq;
using Entities;
using Microsoft.EntityFrameworkCore;
using Saas.Data;
using Saas.Entities;
using Saas.Entities.Dtos;
using Saas.Entities.enums;
using Saas.Repository.Interfaces;

namespace Saas.Repository.Services
{
    public class SecretService(
        ISecretRepository _secretRepository,
    ICollectionSecretRepository _collectionSecretRepository,
    IOrganizationUserRepository _organizationUserRepository,
    ApplicationDbContext _dbContext
    ) : ISecretService
    {
        public async Task<Result> AddSecretWithCollections(SecretWithCollection secretWithCollection)
        {

            var secret = secretWithCollection.Secret;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                await _secretRepository.AddAsync(secret);
                var secretCollections = secretWithCollection.CollectionIds?.Select(collectionIds => new CollectionSecret
                {
                    SecretId = secret.Id,
                    CollectionId = collectionIds
                }).ToList();
                if (secretCollections is not null)
                {
                    await _collectionSecretRepository.AddRangeAsync(secretCollections);
                }
                await transaction.CommitAsync();
            }

            return new Result(true, "Secret added successfully");
        }

        public async Task<Result> UpdateSecretWithCollections(SecretWithCollection secretWithCollection)
        {
            var secret = secretWithCollection.Secret;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {

                await _secretRepository.UpdateAsync(secret);
                var secretCollections = secretWithCollection.CollectionIds?.Select(collectionIds => new CollectionSecret
                {
                    SecretId = secret.Id,
                    CollectionId = collectionIds
                }).ToList();

                if (secretWithCollection.CollectionIds is not null)
                {
                    var existingSecretCollections = await _collectionSecretRepository.GetCollectionsBySecretIdAsync(secretWithCollection.Secret.Id);
                    if (existingSecretCollections is not null)
                    {
                        await _collectionSecretRepository.DeleteRangeAsync(existingSecretCollections);
                    }
                    if (secretCollections is not null)
                    {
                        await _collectionSecretRepository.AddRangeAsync(secretCollections);
                    }
                    await transaction.CommitAsync();
                }
            }

            return new Result(true, "Secret updated successfully");
        }
        public async Task<List<Secret>> GetSecretIncludeAllOrganization(Guid userId)
        {
            var secrets = await _secretRepository.GetAll()
                                .Include(s => s.Organization)
                                    .ThenInclude(o => o.OrganizationUsers.Where(ou => ou.UserId == userId))
                                 .Include(x => x.CollectionSecrets).ThenInclude(c => c.Collection)
                                .Include(s => s.SharedSecrets.Where(ss => ss.SharedTo == userId))
                                .Where(s =>
                                    // Condition 1: Secret belongs to the user
                                    s.UserId == userId ||
                                    // Condition 2: Secret belongs to an organization and the user is its creator
                                    (s.OrganizationId != null && s.Organization.OrganizationUsers.Any(ou => ou.CreatedBy == userId)) ||
                                    // Condition 3: User is part of the organization but not the creator; check if sharedSecrets exist
                                    (s.OrganizationId != null && s.Organization.OrganizationUsers.Any(ou => ou.UserId == userId && ou.Status == OrganizationUserStatus.Accepted && ou.CreatedBy != userId) && s.SharedSecrets.Any(ss => ss.SharedTo == userId)))
                                .OrderBy(s => s.Id)
                                .ThenBy(o => o.Organization.Id)
                                .ToListAsync();

            return secrets;
        }

        public async Task<Secret?> GetSecretByIdIncludeAllOrganization(Guid secretId, Guid userId)
        {
            var secret = await _secretRepository.GetAll()
                        .Include(s => s.Organization)
                            .ThenInclude(o => o.OrganizationUsers)
                        .Include(x => x.CollectionSecrets).ThenInclude(c => c.Collection)
                        .Include(x => x.SharedSecrets)
                         .Where(s => (s.OrganizationId != Guid.Empty &&
                                (s.UserId == userId
                                    || s.Organization.OrganizationUsers.Any(
                                    ou => ou.UserId == userId && ou.Status == OrganizationUserStatus.Accepted
                                    )
                                    ))
                        || (s.OrganizationId == Guid.Empty && s.UserId == userId))
                        .FirstOrDefaultAsync(s => s.Id == secretId);
            return secret;
        }
        public async Task<bool> CheckIfUsersSecret(Guid secretId, Guid userId)
        {
            return await _secretRepository.GetAll()
                        .Include(s => s.Organization)
                            .ThenInclude(o => o.OrganizationUsers)
                         .Where(s => (s.OrganizationId != Guid.Empty && (s.CreatedBy == userId
                        || s.Organization.OrganizationUsers.Any(
                            ou => ou.CreatedBy == userId)
                            ))
                        || (s.OrganizationId == Guid.Empty && s.CreatedBy == userId))
                        .AnyAsync(s => s.Id == secretId);
        }
    }
}