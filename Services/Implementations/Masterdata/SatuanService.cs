using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Helpers;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Requests;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Services.Interfaces;
using inovasyposmobile.Services.Interfaces.Auth;

namespace inovasyposmobile.Services.Implementations.Masterdata
{
    public class SatuanService : IServiceBase<SatuanModel, SearchSortFilterPagingModel>
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private readonly string apiUrl = "masterdata/satuan/";

        public SatuanService(IHttpClientFactory httpClientFactory, IConnectivity connectivity, IAuthService authService)
        {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient("InovasyAPI");
            _connectivity = connectivity;
        }

        public async Task<BaseResponse<SearchResponse<SatuanModel>>?> GetAllAsync(SearchSortFilterPagingModel searchParams)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var token = await _authService.GetTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string searchUrl = apiUrl + "search/";
            string urlWithQuery = searchUrl + QueryStringHelper.ToQueryString(searchParams);
            var response = await _httpClient.GetAsync(urlWithQuery);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat satuan");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<SatuanModel>>>();
        }

        public async Task<BaseResponse<SatuanModel>?> GetByIdAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("Tidak ada koneksi internet");
            }

            var response = await _httpClient.GetAsync($"{apiUrl}getbyid/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat satuan");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SatuanModel>>();
        }

        public async Task<BaseResponse<SatuanModel>?> CreateAsync(SatuanModel satuan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}", satuan);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal menambah satuan");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SatuanModel>>();
        }

        public async Task<BaseResponse<SatuanModel>?> UpdateAsync(string id, SatuanModel satuan)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.PutAsJsonAsync(apiUrl + id, satuan);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal mengupdate satuan");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SatuanModel>>();
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
                throw new ApiException("Gagal menghapus satuan");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
        }

        public async Task<BaseResponse<SatuanModel>?> InitSatuan()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.GetAsync($"{apiUrl}initjenis");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat satuan");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SatuanModel>>();
        }
    }
}