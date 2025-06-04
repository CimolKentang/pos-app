using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Responses;

namespace inovasyposmobile.Services.Interfaces.Masterdata
{
    public interface IProdukService : IServiceBase<ProdukModel, ProdukSearchParams>
    {
        Task<BaseResponse<SearchResponse<ProdukWithStokModel>>?> GetProdukWithStoks(ProdukSearchParams produkSearchParams);
    }
}