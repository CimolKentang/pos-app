using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Responses;

namespace inovasyposmobile.Services.Interfaces.Masterdata
{
    public interface IPelangganService : IServiceBase<PelangganModel, PelangganSearchParams>
    {
        Task<BaseResponse<SearchResponse<ValueDisplayFilterModel>>?> GetAllValueDisplayAsync(PelangganSearchParams searchParams);
    }
}