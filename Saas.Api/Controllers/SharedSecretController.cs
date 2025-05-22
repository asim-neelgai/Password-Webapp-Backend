using Microsoft.AspNetCore.Mvc;
using Saas.Repository.Interfaces;
using AutoMapper;
using Saas.Api.Model;
using Saas.Entities;
using Saas.Repository.Services;
using Saas.Api.Services;
using Saas.Entities.enums;

namespace Saas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SharedSecretController(
        ISharedSecretRepository _sharedSecretRepository,
         IMapper _mapper,
         IConfiguration _configuration,
         ILogger<SharedSecretController> _logger) : BaseController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSharedSecretById(Guid id)
        {
            var sharedSecret = await _sharedSecretRepository.GetByIdAsync(id);

            if (sharedSecret == null)
            {
                return NotFound("Shared secret Not found");
            }
            var SharedSecretToSend = _mapper.Map<SharedSecretResponseModel>(sharedSecret);
            return Ok(SharedSecretToSend);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSharedSecret([FromBody] SharedSecretRequestModel sharedSecretRequestModel,
        IOrganizationService _organizationService,
        ICognitoService _cognitoService,
        IEmailService _emailService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var connectingUser = GetCurrentUserId();

            var usersOrganization = await _organizationService.CheckIfUserIsPartOfOrganization(connectingUser, sharedSecretRequestModel.OrganizationId);
            if (!usersOrganization)
            {
                return BadRequest("Permission denied");
            }
            string accessToken = Request.Headers["Authorization"];
            string token = accessToken.Substring("Bearer ".Length);


            var userInfoResponse = await _cognitoService.GetUserDetailsAsync(token);
            var userEmail = userInfoResponse.UserAttributes?.FirstOrDefault(attr => attr.Name == "email")?.Value;

            var fromEmail = _configuration.GetSection("EMAIL_FROM").Value;
            var frontEndUrl = _configuration.GetSection("FRONTEND_URL").Value;
            if (fromEmail == null)
            {
                _logger.LogError("From SharedSecret: fromEmail not set");
                return BadRequest("From email not set");
            }
            if (frontEndUrl == null)
            {
                _logger.LogError("From SharedSecret: frontEndUrl not set");
                return BadRequest("FrontEndUrl not set");
            }

            foreach (var receiverEmail in sharedSecretRequestModel.SharedToEmails)
            {
                try
                {
                    var response = await _cognitoService.GetUserByEmail(receiverEmail);
                    if (response == null)
                    {
                        var existingOrganizationUser = await _organizationService.CheckIfReceiverHasAccount(sharedSecretRequestModel.OrganizationId, receiverEmail);
                        if (existingOrganizationUser)
                        {
                            // if receiver already has account just resend the email
                            await SendEmailToJoinOrganization(sharedSecretRequestModel, _emailService, userEmail, fromEmail, frontEndUrl, receiverEmail);
                            continue;
                        }
                        else
                        {
                            var sharedSecret = _mapper.Map<SharedSecret>(sharedSecretRequestModel);
                            sharedSecret.CreatedBy = connectingUser;
                            sharedSecret.UpdatedBy = connectingUser;
                            sharedSecret.SharedToEmail = receiverEmail;
                            var res = await _organizationService.AddToOrganizationUserAndSharedSecret(connectingUser, receiverEmail, sharedSecretRequestModel.OrganizationId, sharedSecret);
                            if (res.Success)
                            {
                                await SendEmailToJoinOrganization(sharedSecretRequestModel, _emailService, userEmail, fromEmail, frontEndUrl, receiverEmail);
                            }
                        }
                    }
                    else if (response.UserStatus == "CONFIRMED")
                    {
                        var usersOrganizationDetails = await _organizationService.GetUserOrganizationDetails(sharedSecretRequestModel.OrganizationId, receiverEmail);
                        if (usersOrganizationDetails is null)
                        {
                            var sharedSecret = _mapper.Map<SharedSecret>(sharedSecretRequestModel);
                            sharedSecret.CreatedBy = connectingUser;
                            sharedSecret.UpdatedBy = connectingUser;
                            sharedSecret.SharedToEmail = receiverEmail;
                            var res = await _organizationService.AddToOrganizationUserAndSharedSecret(connectingUser, receiverEmail, sharedSecretRequestModel.OrganizationId, sharedSecret);
                            if (res.Success)
                            {
                                await SendEmailToNotifySharedItem(sharedSecretRequestModel, _emailService, userEmail, fromEmail, frontEndUrl, receiverEmail);
                            }
                        }
                        else
                        {
                            await SendEmailToNotifySharedItem(sharedSecretRequestModel, _emailService, userEmail, fromEmail, frontEndUrl, receiverEmail);
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("From CreateSharedSecret: Foreach: " + e.Message + e.InnerException?.Message);
                    throw;
                }
            }
            return Ok();
        }

        private static async Task SendEmailToNotifySharedItem(SharedSecretRequestModel sharedSecretRequestModel, IEmailService _emailService, string? userEmail, string? fromEmail, string? frontEndUrl, string receiverEmail)
        {
            var subject = $"{userEmail} shared secrets to you";
            var body = $"{userEmail} shared the organization secrets to you. Get full access on fortlock. {frontEndUrl}/vault/?orgid={sharedSecretRequestModel.OrganizationId}&share_invite=true";

            await _emailService.SendEmailAsync(receiverEmail, fromEmail, subject, body);
        }

        private static async Task SendEmailToJoinOrganization(SharedSecretRequestModel sharedSecretRequestModel, IEmailService _emailService, string? userEmail, string fromEmail, string? frontEndUrl, string receiverEmail)
        {
            var subject = $"{userEmail} shared secrets to you";
            var body = $"{userEmail} shared the organization secrets to you. Get full access on fortlock. {frontEndUrl}/register/?orgId={sharedSecretRequestModel.OrganizationId}&email={receiverEmail}&share_invite=true";
            await _emailService.SendEmailAsync(receiverEmail, fromEmail, subject, body);
        }
    }
}