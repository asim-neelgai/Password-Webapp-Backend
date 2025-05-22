using Amazon.CognitoIdentityProvider;

namespace Saas.Repository.Services
{
    using Amazon.CognitoIdentityProvider.Model;
    using Microsoft.Extensions.Configuration;
    using Saas.Repository.Interfaces;

    public class CognitoService : ICognitoService
    {
        private readonly IAmazonCognitoIdentityProvider _cognitoIdentityProvider;
        private readonly IConfiguration _configuration;

        public CognitoService(IAmazonCognitoIdentityProvider cognitoService, IConfiguration configuration)
        {
            _cognitoIdentityProvider = cognitoService;
            _configuration = configuration;
        }
        public async Task<AdminGetUserResponse?> GetUserByEmail(string email)
        {
            try
            {
                var userPoolId = _configuration.GetSection("Cognito:UserPoolId").Value;
                AdminGetUserRequest userRequest = new AdminGetUserRequest
                {
                    Username = email,
                    UserPoolId = userPoolId,
                };

                return await _cognitoIdentityProvider.AdminGetUserAsync(userRequest);
            }
            catch (UserNotFoundException)
            {
                // User not found, return null or handle as needed
                return null;
            }
        }
        public async Task<GetUserResponse> GetUserDetailsAsync(string accessToken)
        {
            var request = new GetUserRequest
            {
                AccessToken = accessToken
            };

            return await _cognitoIdentityProvider.GetUserAsync(request);
        }
    }
}