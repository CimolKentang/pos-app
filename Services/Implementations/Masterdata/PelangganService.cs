using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Services.Implementations.Transaksi;
using inovasyposmobile.Services.Interfaces.Auth;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.Services.Implementations.Masterdata
{
    public class PelangganService : IPelangganService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private readonly string apiUrl = "masterdata/pelanggan/";
        public PelangganService(IHttpClientFactory httpClientFactory, IConnectivity connectivity, IAuthService authService)
        {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient("InovasyCustomerAPI");
            _connectivity = connectivity;
        }

        public async Task<BaseResponse<SearchResponse<PelangganModel>>?> GetAllAsync(PelangganSearchParams searchParams)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var token = await _authService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}search", searchParams);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<PelangganModel>>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Load Pelanggan", ex);
            }
        }

        public async Task<BaseResponse<SearchResponse<ValueDisplayFilterModel>>?> GetAllValueDisplayAsync(PelangganSearchParams searchParams)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var token = await _authService.GetTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}search", searchParams);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<ValueDisplayFilterModel>>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Load Pelanggan", ex);
            }
        }

        public async Task<BaseResponse<PelangganModel>?> GetByIdAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{apiUrl}getbyid/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<PelangganModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Load Pelanggan", ex);
            }
        }

        public async Task<BaseResponse<PelangganModel>?> CreateAsync(PelangganModel pelanggan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}", pelanggan);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<PelangganModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Create Pelanggan", ex);
            }
        }

        public async Task<BaseResponse<PelangganModel>?> UpdateAsync(string id, PelangganModel pelanggan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl + id, pelanggan);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<PelangganModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Update Pelanggan", ex);
            }
        }

        public async Task<BaseResponse<string>?> DeleteAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.DeleteAsync(apiUrl + id);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Delete Pelanggan", ex);
            }
        }
    }
}