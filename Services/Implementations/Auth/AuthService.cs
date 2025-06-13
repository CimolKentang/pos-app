using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Auth;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Services.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;

namespace inovasyposmobile.Services.Implementations.Auth
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly string apiUrl = "token";
        private const string authTokenKey = "auth_token";
        private const string refreshTokenKey = "refresh_token";
        public AuthService(
            IHttpClientFactory httpClientFactory,
            IConnectivity connectivity
        )
        {
            _httpClient = httpClientFactory.CreateClient("InovasyAPI");
            _connectivity = connectivity;
            _tokenHandler = new JwtSecurityTokenHandler();

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "azzampos-api-live.azzamkita.com",
                ValidateAudience = true,
                ValidAudience = "azzampos.azzamkita.com",
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sgEVycaevgrUdrGaBVLYaV-sgEVycaevgrUdrGaBVLYaV-sgEVycaevgrUdrGaBVLYaV")),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }

        public async Task<BaseResponse<TokenModel>?> Login(LoginRequestModel loginRequest)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}", loginRequest);
                response.EnsureSuccessStatusCode();

                var jsonData = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonData);

                return await response.Content.ReadFromJsonAsync<BaseResponse<TokenModel>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApiException("Failed To Login", ex);
            }
        }

        public async Task SetStorageToken(TokenModel token)
        {
            if (token != null && token.Token?.Length > 0)
            {
                SecureStorage.RemoveAll();

                await SecureStorage.SetAsync(authTokenKey, token.Token);
                await SecureStorage.SetAsync(refreshTokenKey, token.RefreshToken!);
            }
            else
            {
                SecureStorage.Remove(authTokenKey);
                SecureStorage.Remove(refreshTokenKey);
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            return await SecureStorage.GetAsync(authTokenKey);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token)) return false;

            try
            {
                _tokenHandler.ValidateToken(token, _tokenValidationParameters, out _);
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }

        public async void Logout()
        {
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync("//LoginRoute");
        }
    }
}