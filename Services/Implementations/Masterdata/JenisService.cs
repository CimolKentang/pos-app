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
using inovasyposmobile.Services.Interfaces.Auth;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.Services.Implementations.Masterdata
{
    public class JenisService : IJenisService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private readonly string apiUrl = "masterdata/jenis/";

        public JenisService(IHttpClientFactory httpClientFactory, IConnectivity connectivity, IAuthService authService)
        {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient("InovasyAPI");
            _connectivity = connectivity;
        }

        public async Task<BaseResponse<SearchResponse<JenisModel>>?> GetAllAsync(SearchSortFilterPagingModel searchParams)
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
                throw new ApiException("Gagal memuat jenis");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<JenisModel>>>();
        }

        public async Task<BaseResponse<JenisModel>?> GetByIdAsync(string id)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("Tidak ada koneksi internet");
            }

            var response = await _httpClient.GetAsync($"{apiUrl}getbyid/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat jenis");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<JenisModel>>();
        }

        public async Task<BaseResponse<JenisModel>?> CreateAsync(JenisModel jenis)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.PostAsJsonAsync($"{apiUrl}", jenis);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal menambah jenis");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<JenisModel>>();
        }

        public async Task<BaseResponse<JenisModel>?> UpdateAsync(string id, JenisModel jenis)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.PutAsJsonAsync(apiUrl + id, jenis);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal mengupdate jenis");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<JenisModel>>();
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
                throw new ApiException("Gagal menghapus jenis");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<string>>();
        }

        public async Task<BaseResponse<JenisModel>?> InitJenis()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            var response = await _httpClient.GetAsync($"{apiUrl}initjenis");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException("Gagal memuat jenis");
            }

            return await response.Content.ReadFromJsonAsync<BaseResponse<JenisModel>>();
        }
    }
}