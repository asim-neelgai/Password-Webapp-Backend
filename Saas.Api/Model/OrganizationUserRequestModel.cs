using System.ComponentModel.DataAnnotations;
using Saas.Entities.enums;

namespace Saas.Api.Model;

public class OrganizationUserRequestModel
{
    public Guid? UserId { get; set; }
    [Required]
    public Guid OrganizationId { get; set; }
    [Required]
    public required string Key { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public required string Email { get; set; }
    public OrganizationUserRole Role { get; set; }
    public OrganizationUserStatus Status { get; set; }

}