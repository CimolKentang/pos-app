using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class ProdukWithStokModel
    {
        public string? GudangId { get; set; }
        public string? NamaGudang { get; set; }
        public string? ProdukId { get; set; }
        public string? TenantId { get; set; }
        public string? ProdukStokId { get; set; }
        public string? NamaProduk { get; set; }
        public string? KodeProduk { get; set; }
        public string? Varian { get; set; }
        public string? Warna { get; set; }
        public string? Ukuran { get; set; }
        public string? Lokasi { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public int? LamaPenggunaan { get; set; }
        public int Berat { get; set; }
        public string? Gambar { get; set; }
        public string? GambarSmall { get; set; }
        public string? ProdukVarian { get; set; }
        public string? ProdukWarnaUkuranVarian { get; set; }
        public string? JenisProduk { get; set; }
        public string? MerekProduk { get; set; }
        public string? SatuanProduk { get; set; }
        public string? SupplierProduk { get; set; }
        public string? DeskripsiProduk { get; set; }
        public decimal HargaModal { get; set; }
        public decimal HargaJual { get; set; }
        public int Saldo { get; set; }

        /// <summary>
        /// Total Qty Masuk
        /// </summary>
        /// <value></value>
        public int Masuk { get; set; }

        /// <summary>
        /// Total Qty Keluar
        /// </summary>
        /// <value></value>
        public int Keluar { get; set; }

        /// <summary>
        /// Saldo * Jumlah Stok
        /// </summary>
        /// <value></value>
        public decimal Nominal { get; set; }
        public JenisModel? Jenis { get; set; }
        public DateTime TanggalMasuk { get; set; }
        public DateTime TanggalExpired { get; set; }
    }
}