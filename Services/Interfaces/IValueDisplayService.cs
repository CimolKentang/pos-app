using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Responses;

namespace inovasyposmobile.Services.Interfaces
{
    public interface IValueDisplayService
    {
        Task<BaseResponse<SearchResponse<ValueDisplayFilterModel>>?> GetAllAsync(string selectMultipleFor, string search);
    }
}