using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Saas.Entities.enums;

namespace Saas.Api.Model
{
    public class SharedSecretRequestModel
    {
        [Required]
        public Guid SecretId { get; set; }
        public SharedSecretAccessLevel AccessLevel { get; set; }
        [Required]
        public required List<string> SharedToEmails { get; set; }
        public Guid OrganizationId { get; set; }
    }
}