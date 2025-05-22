using System.ComponentModel.DataAnnotations;
using Saas.Entities.enums;

namespace Saas.Api.Model;

public class OrganizationUserPutRequestModel
{
    public Guid? UserId { get; set; }
    public OrganizationUserStatus Status { get; set; }

}