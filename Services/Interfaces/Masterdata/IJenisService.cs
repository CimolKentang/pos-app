using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Requests;
using inovasyposmobile.Models.Responses;

namespace inovasyposmobile.Services.Interfaces.Masterdata
{
    public interface IJenisService : IServiceBase<JenisModel, SearchSortFilterPagingModel>
    {
        Task<BaseResponse<JenisModel>?> InitJenis();
    }
}