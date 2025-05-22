using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saas.Api.Model;
using Saas.Api.Services;
using Saas.Entities.enums;
using Saas.Repository.Interfaces;

namespace Saas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrganizationUserController(
    IOrganizationUserRepository _organizationUserRepository,
    IMapper _mapper,
    IConfiguration _configuration) : BaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrganizationUserResponseModel), 200)]
        public async Task<IActionResult> GetOrganizationUserById(Guid id)
        {
            var organizationUser = await _organizationUserRepository.GetByIdAsync(id);

            if (organizationUser == null)
            {
                return NotFound("Organization User not found");
            }
            var OrganizationToSend = _mapper.Map<OrganizationUserResponseModel>(organizationUser);
            return Ok(OrganizationToSend);
        }
        [HttpGet()]
        public async Task<IActionResult> GetOrganizationUserByUserId()
        {
            var connectingUser = GetCurrentUserId();

            var organizationUser = await _organizationUserRepository.GetOrganizationUserByUserIdAsync(connectingUser);

            var organizationUserToSend = _mapper.Map<List<OrganizationUserResponseModel>>(organizationUser);
            return Ok(organizationUserToSend);
        }
        [HttpGet("organizationid/{organizationid}")]
        public async Task<IActionResult> GetOrganizationUserByOrgIdUserId(Guid organizationid)
        {
            var connectingUser = GetCurrentUserId();

            var organizationUser = await _organizationUserRepository.GetOrganizationUserByOrgIdUserIdAsync(organizationid, connectingUser);

            var organizationUserToSend = _mapper.Map<OrganizationUserResponseModel>(organizationUser);
            return Ok(organizationUserToSend);
        }
        [HttpGet("organizationid/all/{organizationid}")]
        public async Task<IActionResult> GetAllOrganizationUsersByOrgIdUserId(Guid organizationid)
        {
            var connectingUser = GetCurrentUserId();

            var organizationUsers = await _organizationUserRepository.GetAllOrganizationUserByOrgIdUserIdAsync(organizationid, connectingUser);

            var organizationUsersToSend = _mapper.Map<List<OrganizationUserResponseModel>>(organizationUsers);
            return Ok(organizationUsersToSend);
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrganizationUser([FromBody] OrganizationUserRequestModel organizationUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var connectingUser = GetCurrentUserId();

            var organizationUser = _mapper.Map<OrganizationUser>(organizationUserModel);
            organizationUser.CreatedBy = connectingUser;
            organizationUser.UpdatedBy = connectingUser;
            organizationUser.InvitingUserId = connectingUser;
            await _organizationUserRepository.AddAsync(organizationUser);
            var organizationUserToSend = _mapper.Map<OrganizationUserResponseModel>(organizationUser);

            return CreatedAtAction(nameof(GetOrganizationUserByUserId), new { id = organizationUserToSend.Id }, organizationUserToSend);
        }

        [HttpPut("{id}/{email}")]
        public async Task<IActionResult> UpdateOrganization(
            Guid id,
             string email,
             [FromBody] OrganizationUserPutRequestModel OrganizationUserModel,
             IEmailService _emailService)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var connectingUser = GetCurrentUserId();

                var usersOrganization = await _organizationUserRepository.GetOrganizationUserByOrgIdEmailAsync(id, email);

                if (usersOrganization is null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "Permission denied");
                }
                var organizationUser = _mapper.Map(OrganizationUserModel, usersOrganization);

                organizationUser.UserId = connectingUser;
                organizationUser.UpdatedBy = connectingUser;
                organizationUser.Status = OrganizationUserModel.Status;
                await _organizationUserRepository.UpdateAsync(organizationUser);

                var frontEndUrl = _configuration.GetSection("FRONTEND_URL").Value;
                if (OrganizationUserModel.Status == OrganizationUserStatus.Accepted)
                {
                    var subject = $"{email} should be confirmed to access the organization";
                    var body = $"{email} should be confirmed so that they can access the organization vault. {frontEndUrl}/vault/?orgid={id}";

                    var fromEmail = _configuration.GetSection("EMAIL_FROM").Value;

                    await _emailService.SendEmailAsync(usersOrganization.Creator?.Email, fromEmail, subject, body);
                }
                else if (OrganizationUserModel.Status == OrganizationUserStatus.Confirmed)
                {
                    var subject = $"{email} has been confirmed to access the organization";
                    var body = $"{email} has been confirmed to access the organization vault. Now you can view shared items in the Organization. {frontEndUrl}/vault/?orgid={id}";

                    var fromEmail = _configuration.GetSection("EMAIL_FROM").Value;

                    await _emailService.SendEmailAsync(usersOrganization.User?.Email, fromEmail, subject, body);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency exception
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Error");
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(Guid id)
        {
            var connectingUser = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingOrganization = await _organizationUserRepository.GetByIdAsync(id);
            if (existingOrganization == null)
            {
                return NotFound("Organization User not found");
            }
            var checkifUsersOrganization = await _organizationUserRepository.CheckOrganizationCreatorAsync(connectingUser, id);

            if (!checkifUsersOrganization)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Permission denied");
            }

            await _organizationUserRepository.DeleteAsync(existingOrganization);

            return NoContent();
        }
    }
}