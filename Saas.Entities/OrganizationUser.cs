using Saas.Entities;
using Saas.Entities.enums;

namespace Entities;

public class OrganizationUser : BaseEntity
{
    public Guid? UserId { get; set; }
    public Guid OrganizationId { get; set; }
    public string? Key { get; set; }
    public string? Email { get; set; }
    public OrganizationUserRole Role { get; set; }
    public OrganizationUserStatus Status { get; set; }
    public Guid? InvitingUserId { get; set; }
    public Organization? Organization { get; set; }
    public User? User { get; set; }
    public User? Creator { get; set; }
}