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
using inovasyposmobile.Services.Interfaces.Transaksi;

namespace inovasyposmobile.Services.Implementations.Transaksi
{
    public class PenjualanService : IPenjualanService
    {
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly string apiUrl = "penjualan/penjualan/";
        public PenjualanService(IHttpClientFactory httpClientFactory, IConnectivity connectivity)
        {
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
                var token = ConstantToken.Token;
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

    public static class ConstantToken
    {
        public static string Token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiNWQzMDI2OTJiNjY2NGY3OGI4OWQwNTJjZGZmMjgyZTgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJlMjIxNTg1OWE1ODA0NmU3YmIyZDIxMTZlNGVjODk3YyIsIm5hbWEiOiJBbndhciBRQSIsImp0aSI6IjdlNWQ2ODBiLTllMzItNGMwMS04ZWY5LTJkZDk2MzQwYmM4NyIsIlVzZXJJZCI6IjVkMzAyNjkyYjY2NjRmNzhiODlkMDUyY2RmZjI4MmU4IiwiVGVuYW50SWQiOiJlMjIxNTg1OWE1ODA0NmU3YmIyZDIxMTZlNGVjODk3YyIsInJvbGUiOiJTZW11YSBBa3NlcyIsIm1vZHVscyI6WyJEYWZ0YXJBa3VuIiwiRGV0YWlsQWt1biIsIkVkaXRBa3VuIiwiSGFwdXNBa3VuIiwiU2VhcmNoQWt1biIsIlRhbWJhaEFrdW4iLCJEYWZ0YXJKZW5pc0FrdW4iLCJEZXRhaWxKZW5pc0FrdW4iLCJFZGl0SmVuaXNBa3VuIiwiU2VhcmNoSmVuaXNBa3VuIiwiVGFtYmFoSmVuaXNBa3VuIiwiRGFmdGFySnVybmFsIiwiRGV0YWlsSnVybmFsIiwiUmVjYWxjdWxhdGVTYWxkb0p1cm5hbCIsIlNlYXJjaEp1cm5hbCIsIkRhZnRhck1hcHBpbmdBa3VuIiwiRGV0YWlsTWFwcGluZ0FrdW4iLCJFZGl0TWFwcGluZ0FrdW4iLCJIYXB1c01hcHBpbmdBa3VuIiwiU2VhcmNoTWFwcGluZ0FrdW4iLCJUYW1iYWhNYXBwaW5nQWt1biIsIkRhZnRhclBhamFrIiwiRGV0YWlsUGFqYWsiLCJFZGl0UGFqYWsiLCJIYXB1c1BhamFrIiwiU2VhcmNoUGFqYWsiLCJUYW1iYWhQYWphayIsIkNhbmNlbFBlbWFzdWthbiIsIkRhZnRhclBlbWFzdWthbiIsIkRldGFpbFBlbWFzdWthbiIsIkVkaXRQZW1hc3VrYW4iLCJTZWFyY2hQZW1hc3VrYW4iLCJUYW1iYWhQZW1hc3VrYW4iLCJDYW5jZWxQZW5nZWx1YXJhbiIsIkRhZnRhclBlbmdlbHVhcmFuIiwiRGV0YWlsUGVuZ2VsdWFyYW4iLCJFZGl0UGVuZ2VsdWFyYW4iLCJTZWFyY2hQZW5nZWx1YXJhbiIsIlRhbWJhaFBlbmdlbHVhcmFuIiwiRGFmdGFyU2FsZG9Bd2FsQWt1biIsIkRldGFpbFNhbGRvQXdhbEFrdW4iLCJFZGl0U2FsZG9Bd2FsQWt1biIsIkhhcHVzU2FsZG9Bd2FsQWt1biIsIlNlYXJjaFNhbGRvQXdhbEFrdW4iLCJUYW1iYWhTYWxkb0F3YWxBa3VuIiwiRGFmdGFyQXV0b21hdGVkQWN0aW9uIiwiRGV0YWlsQXV0b21hdGVkQWN0aW9uIiwiRWRpdEF1dG9tYXRlZEFjdGlvbiIsIkhhcHVzQXV0b21hdGVkQWN0aW9uIiwiU2VhcmNoQXV0b21hdGVkQWN0aW9uIiwiVGFtYmFoQXV0b21hdGVkQWN0aW9uIiwiRGFmdGFyQm9hcmQiLCJEZXRhaWxCb2FyZCIsIkVkaXRCb2FyZCIsIkhhcHVzQm9hcmQiLCJTZWFyY2hCb2FyZCIsIlRhbWJhaEJvYXJkIiwiQWtzZXNDaGF0V2hhdHNBcHAiLCJFZGl0UGVsYW5nZ2FuVGFnV2hhdHNBcHAiLCJIYXB1c0NoYXRXaGF0c0FwcCIsIkthaXRrYW5QZWxhbmdnYW5XaGF0c0FwcCIsIkxlcGFza2FuUGVsYW5nZ2FuV2hhdHNBcHAiLCJEYWZ0YXJNZXNzYWdlQnJvYWRjYXN0IiwiRGV0YWlsTWVzc2FnZUJyb2FkY2FzdCIsIkVkaXRNZXNzYWdlQnJvYWRjYXN0IiwiSGFwdXNNZXNzYWdlQnJvYWRjYXN0IiwiU2VhcmNoTWVzc2FnZUJyb2FkY2FzdCIsIlRhbWJhaE1lc3NhZ2VCcm9hZGNhc3QiLCJEYWZ0YXJQcmlvcml0eVRhc2siLCJEZXRhaWxQcmlvcml0eVRhc2siLCJFZGl0UHJpb3JpdHlUYXNrIiwiSGFwdXNQcmlvcml0eVRhc2siLCJTZWFyY2hQcmlvcml0eVRhc2siLCJUYW1iYWhQcmlvcml0eVRhc2siLCJEYWZ0YXJUYXNrIiwiRGV0YWlsVGFzayIsIkVkaXRUYXNrIiwiSGFwdXNUYXNrIiwiU2VhcmNoVGFzayIsIlRhbWJhaFRhc2siLCJEYWZ0YXJUYXNrTGFiZWwiLCJEZXRhaWxUYXNrTGFiZWwiLCJFZGl0VGFza0xhYmVsIiwiSGFwdXNUYXNrTGFiZWwiLCJTZWFyY2hUYXNrTGFiZWwiLCJUYW1iYWhUYXNrTGFiZWwiLCJEYWZ0YXJUYXNrU3RhdHVzIiwiRGV0YWlsVGFza1N0YXR1cyIsIkVkaXRUYXNrU3RhdHVzIiwiSGFwdXNUYXNrU3RhdHVzIiwiU2VhcmNoVGFza1N0YXR1cyIsIlRhbWJhaFRhc2tTdGF0dXMiLCJEYWZ0YXJUYXNrVHlwZSIsIkRldGFpbFRhc2tUeXBlIiwiRWRpdFRhc2tUeXBlIiwiSGFwdXNUYXNrVHlwZSIsIlNlYXJjaFRhc2tUeXBlIiwiVGFtYmFoVGFza1R5cGUiLCJEYWZ0YXJUZW1wbGF0ZUNoYXQiLCJEZXRhaWxUZW1wbGF0ZUNoYXQiLCJFZGl0VGVtcGxhdGVDaGF0IiwiSGFwdXNUZW1wbGF0ZUNoYXQiLCJTZWFyY2hUZW1wbGF0ZUNoYXQiLCJUYW1iYWhUZW1wbGF0ZUNoYXQiLCJEYXNoYm9hcmRQZW1iZWxpYW4iLCJEYXNoYm9hcmRQZW5qdWFsYW4iLCJEb3dubG9hZExhcG9yYW5CdWt1QmVzYXIiLCJEb3dubG9hZExhcG9yYW5IdXRhbmdQZW1iZWxpYW4iLCJEb3dubG9hZExhcG9yYW5Lb25zaW55YXNpIiwiRG93bmxvYWRMYXBvcmFuS3VuanVuZ2FuS29uc2lueWFzaSIsIkRvd25sb2FkTGFwb3JhbkxhYmFQZW5qdWFsYW5Qcm9kdWsiLCJEb3dubG9hZExhcG9yYW5QZW1iYXlhcmFuSHV0YW5nIiwiRG93bmxvYWRMYXBvcmFuUGVtYmF5YXJhblBpdXRhbmciLCJEb3dubG9hZExhcG9yYW5QZW1iZWxpYW5Qcm9kdWsiLCJEb3dubG9hZExhcG9yYW5QZW1iZWxpYW5TdXBwbGllciIsIkRvd25sb2FkTGFwb3JhblBlbmp1YWxhblBlbGFuZ2dhbiIsIkRvd25sb2FkTGFwb3JhblBlbmp1YWxhblByb2R1ayIsIkRvd25sb2FkTGFwb3JhblBlbmp1YWxhblNhbGVzIiwiRG93bmxvYWRMYXBvcmFuUGVuanVhbGFuV2lsYXlhaCIsIkRvd25sb2FkTGFwb3JhblBlbmp1YWxhbkxhYmEiLCJEb3dubG9hZExhcG9yYW5QZXJzZWRpYWFuS29uc2lueWFzaSIsIkRvd25sb2FkTGFwb3JhblBlcnNlZGlhYW5Qcm9kdWsiLCJEb3dubG9hZExhcG9yYW5QaXV0YW5nUGVuanVhbGFuIiwiRG93bmxvYWRQZW1iYXlhcmFuUGVuanVhbGFuIiwiRG93bmxvYWRQZW1iZWxpYW4iLCJEb3dubG9hZFBlbmp1YWxhbiIsIkRvd25sb2FkUGVuanVhbGFuRm9yU0FQIiwiRG93bmxvYWRSZXR1clBlbmp1YWxhbiIsIkRhZnRhclJvbGUiLCJEZXRhaWxSb2xlIiwiRWRpdFJvbGUiLCJIYXB1c1JvbGUiLCJTZWFyY2hSb2xlIiwiVGFtYmFoUm9sZSIsIkRhZnRhclVzZXIiLCJEZXRhaWxVc2VyIiwiRWRpdFBhc3N3b3JkIiwiRWRpdFVzZXIiLCJDaGFuZ2VQYXNzd29yZFVzZXIiLCJHZW5lcmF0ZUxvZ2luVG9rZW4iLCJIYXB1c1VzZXIiLCJMYXN0QWNjZXNzVXNlciIsIlByb2ZpbGVVc2VyIiwiUmVzZXRQYXNzd29yZCIsIlNlYXJjaFVzZXIiLCJUYW1iYWhVc2VyIiwiRGFmdGFyVXNlclJvbGUiLCJEZXRhaWxVc2VyUm9sZSIsIkVkaXRVc2VyUm9sZSIsIkhhcHVzVXNlclJvbGUiLCJTZWFyY2hVc2VyUm9sZSIsIlRhbWJhaFVzZXJSb2xlIiwiRGFmdGFyVXNlclRva2VuIiwiRGV0YWlsVXNlclRva2VuIiwiRWRpdFVzZXJUb2tlbiIsIkhhcHVzVXNlclRva2VuIiwiU2VhcmNoVXNlclRva2VuIiwiVGFtYmFoVXNlclRva2VuIiwiSW1wb3J0VHJhbnNha3NpSHV0YW5nIiwiSW1wb3J0VHJhbnNha3NpUGl1dGFuZyIsIkltcG9ydFRyYW5zYWtzaVN0b2tPcG5hbWUiLCJJbXBvcnRXaWxheWFoIiwiSW1wb3J0V2lsYXlhaEt1cmlyU0FQIiwiQ2FuY2VsS29uc2lueWFzaSIsIkRhZnRhcktvbnNpbnlhc2kiLCJEZXRhaWxLb25zaW55YXNpIiwiUGVsYW5nZ2FuQmFueWFrS29uc2lueWFzaSIsIlNlYXJjaEtvbnNpbnlhc2kiLCJUYW1iYWhLb25zaW55YXNpIiwiRGFmdGFyS29uc2lueWFzaVJlZmlsbCIsIkRldGFpbEtvbnNpbnlhc2lSZWZpbGwiLCJTZWFyY2hLb25zaW55YXNpUmVmaWxsIiwiVGFtYmFoS29uc2lueWFzaVJlZmlsbCIsIkRhZnRhcktvbnNpbnlhc2lSZXR1ciIsIkRldGFpbEtvbnNpbnlhc2lSZXR1ciIsIlNlYXJjaEtvbnNpbnlhc2lSZXR1ciIsIlRhbWJhaEtvbnNpbnlhc2lSZXR1ciIsIkNhbmNlbEt1bmp1bmdhbktvbnNpbnlhc2kiLCJEYWZ0YXJLdW5qdW5nYW5Lb25zaW55YXNpIiwiRGV0YWlsS3VuanVuZ2FuS29uc2lueWFzaSIsIkVkaXRLdW5qdW5nYW5Lb25zaW55YXNpIiwiU2VhcmNoS3VuanVuZ2FuS29uc2lueWFzaSIsIlRhbWJhaEt1bmp1bmdhbktvbnNpbnlhc2kiLCJMYXBvcmFuQnVrdUJlc2FyIiwiTGFwb3JhbkhhcmlhbiIsIkxhcG9yYW5MYWJhUnVnaSIsIkxhcG9yYW5OZXJhY2EiLCJMYXBvcmFuS29uc2lueWFzaSIsIkxhcG9yYW5LdW5qdW5nYW5Lb25zaW55YXNpIiwiTGFwb3JhbkRpc2tvblBlbWJlbGlhbiIsIkxhcG9yYW5IdXRhbmdQZW1iZWxpYW4iLCJMYXBvcmFuUGVtYmF5YXJhbkh1dGFuZyIsIkxhcG9yYW5QZW1iZWxpYW5Qcm9kdWsiLCJMYXBvcmFuUGVtYmVsaWFuU3VwcGxpZXIiLCJMYXBvcmFuRGlza29uUGVuanVhbGFuIiwiTGFwb3JhbkxhYmFQZW5qdWFsYW4iLCJMYXBvcmFuTGFiYVBlbmp1YWxhblByb2R1ayIsIkxhcG9yYW5QZW1iYXlhcmFuUGl1dGFuZyIsIkxhcG9yYW5QZW5qdWFsYW5QZWxhbmdnYW4iLCJMYXBvcmFuUGVuanVhbGFuUHJvZHVrIiwiTGFwb3JhblBlbmp1YWxhblNhbGVzIiwiTGFwb3JhblBlbmp1YWxhbldpbGF5YWgiLCJMYXBvcmFuUGl1dGFuZ1Blbmp1YWxhbiIsIkxhcG9yYW5QZXJzZWRpYWFuUHJvZHVrIiwiRGF0YU1haW50ZW5hbmNlIiwiRGFmdGFySGFyZ2FHcm9zaXIiLCJEZXRhaWxIYXJnYUdyb3NpciIsIkVkaXRIYXJnYUdyb3NpciIsIkhhcHVzSGFyZ2FHcm9zaXIiLCJTZWFyY2hIYXJnYUdyb3NpciIsIlRhbWJhaEhhcmdhR3Jvc2lyIiwiRGFmdGFyR3JvdXBQZWxhbmdnYW4iLCJEZXRhaWxHcm91cFBlbGFuZ2dhbiIsIkVkaXRHcm91cFBlbGFuZ2dhbiIsIkhhcHVzR3JvdXBQZWxhbmdnYW4iLCJTZWFyY2hHcm91cFBlbGFuZ2dhbiIsIlRhbWJhaEdyb3VwUGVsYW5nZ2FuIiwiRGFmdGFyR3VkYW5nIiwiRGV0YWlsR3VkYW5nIiwiRWRpdEd1ZGFuZyIsIkhhcHVzR3VkYW5nIiwiU2VhcmNoR3VkYW5nIiwiVGFtYmFoR3VkYW5nIiwiRGFmdGFySmFiYXRhbiIsIkRldGFpbEphYmF0YW4iLCJFZGl0SmFiYXRhbiIsIkhhcHVzSmFiYXRhbiIsIlNlYXJjaEphYmF0YW4iLCJUYW1iYWhKYWJhdGFuIiwiRGFmdGFySmVuaXMiLCJEZXRhaWxKZW5pcyIsIkVkaXRKZW5pcyIsIkhhcHVzSmVuaXMiLCJTZWFyY2hKZW5pcyIsIlRhbWJhaEplbmlzIiwiRGFmdGFyS2FyeWF3YW4iLCJEZXRhaWxLYXJ5YXdhbiIsIkVkaXRLYXJ5YXdhbiIsIkhhcHVzS2FyeWF3YW4iLCJTZWFyY2hLYXJ5YXdhbiIsIlRhbWJhaEthcnlhd2FuIiwiRGFmdGFyS3VyaXIiLCJEZXRhaWxLdXJpciIsIkVkaXRLdXJpciIsIkhhcHVzS3VyaXIiLCJTZWFyY2hLdXJpciIsIlRhbWJhaEt1cmlyIiwiRGFmdGFyTWVyZWsiLCJEZXRhaWxNZXJlayIsIkVkaXRNZXJlayIsIkhhcHVzTWVyZWsiLCJTZWFyY2hNZXJlayIsIlRhbWJhaE1lcmVrIiwiRGFmdGFyTWV0b2RlUGVtYmF5YXJhbiIsIkRldGFpbE1ldG9kZVBlbWJheWFyYW4iLCJFZGl0TWV0b2RlUGVtYmF5YXJhbiIsIkhhcHVzTWV0b2RlUGVtYmF5YXJhbiIsIlNlYXJjaE1ldG9kZVBlbWJheWFyYW4iLCJUYW1iYWhNZXRvZGVQZW1iYXlhcmFuIiwiRGFmdGFyUGVsYW5nZ2FuIiwiRGV0YWlsUGVsYW5nZ2FuIiwiRG93bmxvYWRQZWxhbmdnYW4iLCJFZGl0UGVsYW5nZ2FuIiwiSGFwdXNQZWxhbmdnYW4iLCJJbXBvcnRQZWxhbmdnYW4iLCJJbmZvcm1hc2lDUk1QZWxhbmdnYW4iLCJTZWFyY2hQZWxhbmdnYW4iLCJUYW1iYWhQZWxhbmdnYW4iLCJEYWZ0YXJQcm9kdWsiLCJEZXRhaWxQcm9kdWsiLCJEb3dubG9hZFByb2R1ayIsIkVkaXRQcm9kdWsiLCJIYXB1c1Byb2R1ayIsIkltcG9ydFByb2R1ayIsIlByaW50UVJDb2RlUHJvZHVrIiwiU2VhcmNoUHJvZHVrIiwiVGFtYmFoUHJvZHVrIiwiRGFmdGFyU2FsZXNDaGFubmVsIiwiRGV0YWlsU2FsZXNDaGFubmVsIiwiRWRpdFNhbGVzQ2hhbm5lbCIsIkhhcHVzU2FsZXNDaGFubmVsIiwiU2VhcmNoU2FsZXNDaGFubmVsIiwiVGFtYmFoU2FsZXNDaGFubmVsIiwiRGFmdGFyU2F0dWFuIiwiRGV0YWlsU2F0dWFuIiwiRWRpdFNhdHVhbiIsIkhhcHVzU2F0dWFuIiwiU2VhcmNoU2F0dWFuIiwiVGFtYmFoU2F0dWFuIiwiRGFmdGFyU3VwcGxpZXIiLCJEZXRhaWxTdXBwbGllciIsIkVkaXRTdXBwbGllciIsIkhhcHVzU3VwcGxpZXIiLCJTZWFyY2hTdXBwbGllciIsIlRhbWJhaFN1cHBsaWVyIiwiRGFmdGFyVGFnUGVsYW5nZ2FuIiwiRGV0YWlsVGFnUGVsYW5nZ2FuIiwiRWRpdFRhZ1BlbGFuZ2dhbiIsIkhhcHVzVGFnUGVsYW5nZ2FuIiwiU2VhcmNoVGFnUGVsYW5nZ2FuIiwiVGFtYmFoVGFnUGVsYW5nZ2FuIiwiRGFmdGFyVGlwZVBlbGFuZ2dhbiIsIkRldGFpbFRpcGVQZWxhbmdnYW4iLCJFZGl0VGlwZVBlbGFuZ2dhbiIsIkhhcHVzVGlwZVBlbGFuZ2dhbiIsIlNlYXJjaFRpcGVQZWxhbmdnYW4iLCJUYW1iYWhUaXBlUGVsYW5nZ2FuIiwiRGFmdGFyV2lsYXlhaCIsIkRldGFpbFdpbGF5YWgiLCJFZGl0V2lsYXlhaCIsIlNlYXJjaFdpbGF5YWgiLCJUYW1iYWhXaWxheWFoIiwiQmF5YXJJbnZvaWNlUGVtYmVsaWFuIiwiQ2FuY2VsSW52b2ljZVBlbWJlbGlhbiIsIkRhZnRhclBlbWJheWFyYW5QZW1iZWxpYW4iLCJEZXRhaWxQZW1iYXlhcmFuUGVtYmVsaWFuIiwiU2VhcmNoUGVtYmF5YXJhblBlbWJlbGlhbiIsIlRhbWJhaFBlbWJheWFyYW5QZW1iZWxpYW4iLCJDYW5jZWxQZW1iZWxpYW4iLCJEYWZ0YXJQZW1iZWxpYW4iLCJEZXRhaWxQZW1iZWxpYW4iLCJFZGl0UGVtYmVsaWFuIiwiUHJvc2VzUGVtYmVsaWFuIiwiU2VhcmNoUGVtYmVsaWFuIiwiVGFtYmFoUGVtYmVsaWFuIiwiVXBkYXRlU3RhdHVzUGVtYmVsaWFuIiwiQmF5YXJJbnZvaWNlIiwiQ2FuY2VsSW52b2ljZSIsIkRhZnRhclBlbWJheWFyYW5QZW5qdWFsYW4iLCJEZXRhaWxQZW1iYXlhcmFuUGVuanVhbGFuIiwiU2VhcmNoUGVtYmF5YXJhblBlbmp1YWxhbiIsIlRhbWJhaFBlbWJheWFyYW5QZW5qdWFsYW4iLCJUZXJpbWFJbnZvaWNlIiwiVXBsb2FkUmVmZXJlbnNpQmF5YXJQaXV0YW5nUGVuanVhbGFuIiwiQmlzYUtpcmltUGVuanVhbGFuIiwiQ2FuY2VsUGVuanVhbGFuIiwiQ2FuY2VsUGVzYW5hblBlbmp1YWxhbiIsIkRhZnRhclBlbmp1YWxhbiIsIkRldGFpbFBlbmp1YWxhbiIsIkFsbG93RWRpdEhhcmdhSnVhbFBlbmp1YWxhbiIsIkVkaXRQZW5qdWFsYW4iLCJFZGl0UGVzYW5hblBlbmp1YWxhbiIsIk11bGFpUGVuZ2VtYXNhblBlbmp1YWxhbiIsIk11bGFpUGVuZ2VtYXNhblRhbWJhaFBlbmp1YWxhbiIsIlByb3Nlc1BlbmdpcmltYW5QZW5qdWFsYW4iLCJTZWFyY2hQZW5qdWFsYW4iLCJDb21wbGV0ZWRTdGF0dXNQZW5qdWFsYW4iLCJTdWRhaERpa2VtYXNQZW5qdWFsYW4iLCJUYW1iYWhQZW5qdWFsYW4iLCJUYW1iYWhQZW5qdWFsYW5LYXNpciIsIlRhbWJhaFBlbmp1YWxhbktvbnNpbnlhc2kiLCJVcGRhdGVLZXRlcmFuZ2FuUGVuZ2lyaW1hblBlbmp1YWxhbiIsIlVwZGF0ZVJlc2lQZW5qdWFsYW4iLCJVcGRhdGVTdGF0dXNQZW5qdWFsYW4iLCJVcGRhdGVTdGF0dXNQZW5qdWFsYW5CYW55YWsiLCJUZXJraXJpbVBlbmp1YWxhbiIsIlVwbG9hZFJlc2lQZW5qdWFsYW4iLCJEYWZ0YXJLYXJ0dVN0b2siLCJEZXRhaWxLYXJ0dVN0b2siLCJEb3dubG9hZEthcnR1U3RvayIsIlNlYXJjaEthcnR1U3RvayIsIkRhZnRhclByb2R1a1N0b2siLCJEZXRhaWxQcm9kdWtTdG9rIiwiSGFwdXNQcm9kdWtTdG9rIiwiU2VhcmNoUHJvZHVrU3RvayIsIlRhbWJhaFByb2R1a1N0b2siLCJEYWZ0YXJTdG9rT3BuYW1lIiwiRGV0YWlsU3Rva09wbmFtZSIsIkRvd25sb2FkU3Rva09wbmFtZSIsIkVkaXRTdG9rT3BuYW1lIiwiUHJvc2VzU3Rva09wbmFtZSIsIlNlYXJjaFN0b2tPcG5hbWUiLCJBbGxvd1NPR3VkYW5nS29uc2lueWFzaSIsIlRhbWJhaFN0b2tPcG5hbWUiLCJEYWZ0YXJTdG9rVHJhbnNmZXIiLCJEZXRhaWxTdG9rVHJhbnNmZXIiLCJTZWFyY2hTdG9rVHJhbnNmZXIiLCJUYW1iYWhTdG9rVHJhbnNmZXIiLCJCYXlhckludm9pY2VSZXR1clBlbWJlbGlhbiIsIkNhbmNlbEludm9pY2VSZXR1clBlbWJlbGlhbiIsIkRhZnRhclBlbWJheWFyYW5SZXR1clBlbWJlbGlhbiIsIkRldGFpbFBlbWJheWFyYW5SZXR1clBlbWJlbGlhbiIsIlNlYXJjaFBlbWJheWFyYW5SZXR1clBlbWJlbGlhbiIsIlRhbWJhaFBlbWJheWFyYW5SZXR1clBlbWJlbGlhbiIsIkNhbmNlbFJldHVyUGVtYmVsaWFuIiwiQ29tcGxldGVkU3RhdHVzUmV0dXJQZW1iZWxpYW4iLCJEYWZ0YXJSZXR1clBlbWJlbGlhbiIsIkRldGFpbFJldHVyUGVtYmVsaWFuIiwiUHJvc2VzUmV0dXJQZW1iZWxpYW4iLCJTZWFyY2hSZXR1clBlbWJlbGlhbiIsIlRhbWJhaFJldHVyUGVtYmVsaWFuIiwiQmF5YXJJbnZvaWNlUmV0dXJQZW5qdWFsYW4iLCJDYW5jZWxJbnZvaWNlUmV0dXJQZW5qdWFsYW4iLCJEYWZ0YXJQZW1iYXlhcmFuUmV0dXJQZW5qdWFsYW4iLCJEZXRhaWxQZW1iYXlhcmFuUmV0dXJQZW5qdWFsYW4iLCJTZWFyY2hQZW1iYXlhcmFuUmV0dXJQZW5qdWFsYW4iLCJUYW1iYWhQZW1iYXlhcmFuUmV0dXJQZW5qdWFsYW4iLCJDYW5jZWxSZXR1clBlbmp1YWxhbiIsIkRhZnRhclJldHVyUGVuanVhbGFuIiwiRGV0YWlsUmV0dXJQZW5qdWFsYW4iLCJQcm9zZXNSZXR1clBlbmp1YWxhbiIsIlNlYXJjaFJldHVyUGVuanVhbGFuIiwiVGFtYmFoUmV0dXJQZW5qdWFsYW4iLCJEYWZ0YXJUZW5hbnQiLCJEZXRhaWxUZW5hbnQiLCJUZW5hbnRFZGl0UHJvZmlsZSIsIlRlbmFudEVkaXRTZXR0aW5nIiwiRWRpdFRlbmFudCIsIkhhcHVzVGVuYW50IiwiVGVuYW50UHJvZmlsZSIsIlNlYXJjaFRlbmFudCIsIlRhbWJhaFRlbmFudCJdLCJuYmYiOjE3NDkwODA4NjgsImV4cCI6MTc0OTEyNDA2OCwiaWF0IjoxNzQ5MDgwODY4LCJpc3MiOiJhenphbXBvcy1hcGktbGl2ZS5henphbWtpdGEuY29tIiwiYXVkIjoiYXp6YW1wb3MuYXp6YW1raXRhLmNvbSJ9.6bMoQlCI3VSKm5smshWZDyAh2pmoB9pKTj3jvwyDaDpdNcd5XzZ_s6i5vC-0zrRR4N-DZbCHpxkR4bLl6ieqRg";
    }
}