using System.ComponentModel.DataAnnotations;

namespace Saas.Api.Model
{
    public class OrganizationRequestModel
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public required string Key { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}