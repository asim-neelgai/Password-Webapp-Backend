using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Saas.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public required string Email { get; set; }
        public Guid? PaymentPlanId { get; set; }
        public required string Key { get; set; }
        public required string PublicKey { get; set; }
        public required string PrivateKey { get; set; }
        public PaymentPlan? PaymentPlan { get; set; }
        public ICollection<Secret>? Secrets { get; set; }
        public ICollection<Payment>? PaymentsInitiated { get; set; }
        public ICollection<Payment>? PaymentsVerified { get; set; }
        public ICollection<SharedSecret>? SharedSecrets { get; set; }
        public ICollection<OrganizationUser>? OrganizationCreatorUsers { get; set; }
        public ICollection<OrganizationUser>? OrganizationUsers { get; set; }

    }
}