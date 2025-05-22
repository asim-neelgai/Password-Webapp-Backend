using Entities;
using Saas.Entities.enums;

namespace Saas.Entities
{
    public class SharedSecret : BaseEntity
    {
        public Guid SecretId { get; set; }
        public SharedSecretAccessLevel AccessLevel { get; set; }
        public Guid? SharedTo { get; set; }
        public required string SharedToEmail { get; set; }
        public bool HasAllowedEdit { get; set; }
        public Secret? Secret { get; set; }
        public User? User { get; set; }
    }
}