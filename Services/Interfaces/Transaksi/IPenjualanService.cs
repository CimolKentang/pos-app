using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Models.Transaksi.Penjualan;

namespace inovasyposmobile.Services.Interfaces.Transaksi
{
    public interface IPenjualanService : IServiceBase<PenjualanModel, PenjualanSearchParams>
    {
        Task<BaseResponse<PenjualanModel>?> InitPenjualanKasir();
    }
}