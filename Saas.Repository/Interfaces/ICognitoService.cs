using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;

namespace Saas.Repository.Interfaces
{
    public interface ICognitoService
    {
        Task<AdminGetUserResponse?> GetUserByEmail(string email);
        Task<GetUserResponse> GetUserDetailsAsync(string accessToken);
    }
}