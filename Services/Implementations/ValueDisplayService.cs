using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Helpers;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Services.Implementations.Transaksi;
using inovasyposmobile.Services.Interfaces;
using inovasyposmobile.Services.Interfaces.Auth;

namespace inovasyposmobile.Services.Implementations
{
    public class ValueDisplayService : IValueDisplayService
    {
        private readonly HttpClient _httpPosClient;
        private readonly HttpClient _httpCustomerClient;
        private readonly IConnectivity _connectivity;
        private readonly IAuthService _authService;
        private string apiUrl = "";
        public ValueDisplayService(
            IHttpClientFactory httpClientFactory,
            IConnectivity connectivity,
            IAuthService authService
        ) {
            _authService = authService;
            _httpPosClient = httpClientFactory.CreateClient("InovasyAPI");
            _httpCustomerClient = httpClientFactory.CreateClient("InovasyCustomerAPI");
            _connectivity = connectivity;
        }

        public async Task<BaseResponse<SearchResponse<ValueDisplayFilterModel>>?> GetAllAsync(string selectMultipleFor, string search)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new InternetException("No Internet Connection");
            }

            try
            {
                var token = await _authService.GetTokenAsync();

                object searchParamsObject = new object();
                HttpResponseMessage response;

                if (selectMultipleFor.ToLower().Contains(SelectMultipleForConstant.Pelanggan.ToLower()))
                {
                    _httpCustomerClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    _httpPosClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                switch (selectMultipleFor)
                {
                    case SelectMultipleForConstant.Pelanggan:
                        searchParamsObject = new PelangganSearchParams();
                        ((PelangganSearchParams)searchParamsObject).IsValueDisPlay = true;
                        ((PelangganSearchParams)searchParamsObject).Search = search;
                        apiUrl = "masterdata/pelanggan/";
                        response = await _httpCustomerClient.PostAsJsonAsync($"{apiUrl}search", searchParamsObject);
                        break;

                    case SelectMultipleForConstant.Gudang:
                        searchParamsObject = new GudangSearchParams();
                        ((GudangSearchParams)searchParamsObject).IsValueDisPlay = true;
                        ((GudangSearchParams)searchParamsObject).Search = search;
                        apiUrl = "masterdata/gudang/search";
                        string urlWithQuery = apiUrl + QueryStringHelper.ToQueryString((GudangSearchParams)searchParamsObject);
                        response = await _httpPosClient.GetAsync(urlWithQuery);
                        break;
                        
                    default:
                        searchParamsObject = new PelangganSearchParams();
                        ((PelangganSearchParams)searchParamsObject).IsValueDisPlay = true;
                        ((PelangganSearchParams)searchParamsObject).Search = search;
                        apiUrl = "masterdata/pelanggan/";
                        response = await _httpCustomerClient.PostAsJsonAsync($"{apiUrl}search", searchParamsObject);
                        break;
                }

                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                return await response.Content.ReadFromJsonAsync<BaseResponse<SearchResponse<ValueDisplayFilterModel>>>();
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("Failed To Load Data", ex);
            }
        }
    }
}