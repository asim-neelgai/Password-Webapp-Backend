using Entities;

namespace Saas.Api.Model
{
    public class SecretWithCollectionsRequestModel
    {
        public required SecretModel Secret { get; set; }
        public Guid? OrganizationId { get; set; }
        public List<Guid>? CollectionIds { get; set; }
    }
}