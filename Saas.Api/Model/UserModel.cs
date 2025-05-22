using System.ComponentModel.DataAnnotations;

namespace Saas.Api.Model
{
    public class UserModel
    {
        public Guid? UserId { get; set; }
        public Guid? PaymentPlanId { get; set; }
        [Required]
        public required string Key { get; set; }
        public required string PublicKey { get; set; }
        public required string PrivateKey { get; set; }
    }
}