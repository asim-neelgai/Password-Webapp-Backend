using System.ComponentModel.DataAnnotations;
using Entities;
using Saas.Entities;
using Saas.Entities.enums;

namespace Saas.Api.Model;

public class SecretResponseModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public SecretType Type { get; set; }
    public required string Content { get; set; }
    public bool IsShared { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public Guid? OrganizationId { get; set; }
    public IEnumerable<OrganizationUser> OrganizationUsers { get; set; } = [];
    public IEnumerable<SharedSecret> SharedSecrets { get; set; } = [];
    public IEnumerable<CollectionSecretModel> CollectionSecretModels { get; set; } = [];
}