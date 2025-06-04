using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Persediaan;

namespace inovasyposmobile.Models.Transaksi.Penjualan
{
    public class PenjualanDetailModel
    {
        public string? PenjualanDetailId { get; set; }

        public string? TenantId { get; set; }

        public string? PenjualanId { get; set; }

        public string? ProdukId { get; set; }

        public string? ProdukStokId { get; set; }

        public string? NamaProduk { get; set; }

        public string? Gambar { get; set; }

        public string? GambarSmall { get; set; }

        public string? DeskripsiProduk { get; set; }

        public string? JenisProduk { get; set; }

        public string? MerekProduk { get; set; }

        public string? SatuanProduk { get; set; }

        public string? SupplierProduk { get; set; }

        public int Jumlah { get; set; }

        public decimal HargaModal { get; set; }

        public decimal HargaJual { get; set; }

        public decimal HargaJualProduk { get; set; }

        public decimal? DiskonPersentase { get; set; }

        public decimal Diskon { get; set; }

        public bool IsDiskonProductAmount { get; set; }

        public bool IsDiskonProductPercent { get; set; }

        public string? DeskripsiDiskon { get; set; }

        public decimal? SubTotal { get; set; }
        public string? Varian { get; set; }

        public string? RecordStatus { get; set; }

        public string? ProdukVarian { get; set; }

        public int Berat { get; set; }
    }
}