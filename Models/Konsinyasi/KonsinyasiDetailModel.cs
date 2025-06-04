using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Persediaan;

namespace inovasyposmobile.Models.Konsinyasi
{
    public class KonsinyasiDetailModel : BaseModel
    {
        public string? KonsinyasiDetailId { get; set; }

        public string? TenantId { get; set; }

        public string? KonsinyasiId { get; set; }

        /// <summary>
        /// Tanggal Masuk Produk
        /// </summary>
        /// <value></value>

        public DateTime TanggalMasuk { get; set; }
        public string? ProdukId { get; set; }
        public string? ProdukStokId { get; set; }
        public string? NamaProduk { get; set; }
        public string? DeskripsiProduk { get; set; }
        public string? JenisProduk { get; set; }

        public string? MerekProduk { get; set; }

        public string? SatuanProduk { get; set; }

        public string? SupplierProduk { get; set; }

        public int Jumlah { get; set; }

        public decimal HargaModal { get; set; }

        public decimal HargaJual { get; set; }

        public decimal Diskon { get; set; }

        public string? DeskripsiDiskon { get; set; }

        public decimal SubTotal { get; set; }

        public string? Varian { get; set; }

        public string? ProdukVarian { get; set; }

        public virtual KonsinyasiModel? Konsinyasi { get; set; }

        public virtual ProdukModel? Produk { get; set; }

        public virtual ProdukStokModel? ProdukStok { get; set; }
    }
}