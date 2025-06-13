using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Auth;
using inovasyposmobile.Models.Responses;

namespace inovasyposmobile.Services.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<BaseResponse<TokenModel>?> Login(LoginRequestModel loginRequest);
        Task SetStorageToken(TokenModel token);
        Task<string?> GetTokenAsync();
        Task<bool> IsAuthenticatedAsync();
        void Logout();
    }
}