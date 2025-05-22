using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Saas.Entities.Dtos;

namespace Saas.Repository.Interfaces
{
    public interface ISecretService
    {
        Task<Result> AddSecretWithCollections(SecretWithCollection secretWithCollection);
        Task<Result> UpdateSecretWithCollections(SecretWithCollection secretWithCollection);
        Task<List<Secret>> GetSecretIncludeAllOrganization(Guid userId);
        Task<Secret?> GetSecretByIdIncludeAllOrganization(Guid secretId, Guid userId);
        Task<bool> CheckIfUsersSecret(Guid secretId, Guid userId);
    }
}