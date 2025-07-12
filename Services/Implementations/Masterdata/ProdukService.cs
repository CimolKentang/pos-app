using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Services.Implementations.Transaksi;
using inovasyposmobile.Services.Interfaces.Auth;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.Services.Implementations.Masterdata
{
    public class ProdukService : IProdukService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private readonly string apiUrl = "masterdata/produk/";
        public ProdukService(IHttpClientFactory httpClientFactory, IConnectivity connectivity, IAuthService authService)
        {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient("InovasyAPI");
            _connectivity = connectivity;
        }

        public async Task<BaseResponse<SearchResponse<ProdukModel>>?> GetAllAsync(ProdukSearchParams searchParams)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}search", searchParams);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat produk");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<ProdukModel>>>();
        }

        public async Task<BaseResponse<ProdukModel>?> GetByIdAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.GetAsync($"{apiUrl}getbyid/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat produk");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<ProdukModel>>();
        }

        public async Task<BaseResponse<ProdukModel>?> CreateAsync(ProdukModel pelanggan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}", pelanggan);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal menambah produk");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<ProdukModel>>();
        }

        public async Task<BaseResponse<ProdukModel>?> UpdateAsync(string id, ProdukModel pelanggan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.PutAsJsonAsync(apiUrl + id, pelanggan);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal mengupdate produk");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<ProdukModel>>();
        }

        public async Task<BaseResponse<string>?> DeleteAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.DeleteAsync(apiUrl + id);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal menghapus produk");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
        }

        public async Task<BaseResponse<SearchResponse<ProdukWithStokModel>>?> GetProdukWithStoks(ProdukSearchParams searchParams)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}search-produk-stok", searchParams);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat produk");
            }
                
            return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<ProdukWithStokModel>>>();
        }
    }
}