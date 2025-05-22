using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saas.Entities.enums;

namespace Saas.Api.Model
{
    public class SharedSecretResponseModel
    {
        public Guid SecretId { get; set; }
        public SharedSecretAccessLevel AccessLevel { get; set; }
        public Guid SharedTo { get; set; }
        public bool HasAllowedEdit { get; set; }
    }
}