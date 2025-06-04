using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Responses;

namespace inovasyposmobile.Services.Interfaces
{
    public interface IServiceBase<TModel, TSearchParams>
    {
        Task<BaseResponse<SearchResponse<TModel>>?> GetAllAsync(TSearchParams searchParams);
        Task<BaseResponse<TModel>?> GetByIdAsync(string id);
        Task<BaseResponse<TModel>?> CreateAsync(TModel model);
        Task<BaseResponse<TModel>?> UpdateAsync(string id, TModel model);
        Task<BaseResponse<string>?> DeleteAsync(string id);
    }
}