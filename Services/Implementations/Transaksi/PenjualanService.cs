using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Models.Transaksi;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Interfaces.Auth;
using inovasyposmobile.Services.Interfaces.Transaksi;

namespace inovasyposmobile.Services.Implementations.Transaksi
{
    public class PenjualanService : IPenjualanService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private readonly string apiUrl = "penjualan/penjualan/";
        public PenjualanService(
            IHttpClientFactory httpClientFactory,
            IConnectivity connectivity,
            IAuthService authService
        ) {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient("InovasyAPI");
            _connectivity = connectivity;
        }

        public async Task<BaseResponse<SearchResponse<PenjualanModel>>?> GetAllAsync(PenjualanSearchParams searchParams)
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

                return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<PenjualanModel>>>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApiException("Failed To Load Penjualan", ex);
            }
        }

        public async Task<BaseResponse<PenjualanModel>?> GetByIdAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{apiUrl}getbyid/{id}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<BaseResponse<PenjualanModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Load Penjualan", ex);
            }
        }

        public async Task<BaseResponse<PenjualanModel>?> CreateAsync(PenjualanModel penjualan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{apiUrl}", penjualan);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<PenjualanModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Create Penjualan", ex);
            }
        }

        public async Task<BaseResponse<PenjualanModel>?> UpdateAsync(string id, PenjualanModel penjualan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync(apiUrl + id, penjualan);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<BaseResponse<PenjualanModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Update Penjualan", ex);
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
                throw new ApiException("Failed To Delete Penjualan", ex);
            }
        }

        public async Task<BaseResponse<PenjualanModel>?> InitPenjualanKasir()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var response = await _httpClient.GetAsync($"{apiUrl}initpenjualankasir");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<BaseResponse<PenjualanModel>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Init Penjualan Kasir", ex);
            }
        }
    }
}