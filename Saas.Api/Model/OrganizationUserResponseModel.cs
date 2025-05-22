using System.ComponentModel.DataAnnotations;
using Saas.Entities.enums;

namespace Saas.Api.Model;

public class OrganizationUserResponseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public required string Key { get; set; }
    public required string Email { get; set; }
    public OrganizationUserRole Role { get; set; }
    public OrganizationUserStatus Status { get; set; }
    public Guid? InvitingUserId { get; set; }

}