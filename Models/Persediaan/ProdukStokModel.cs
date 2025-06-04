using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;

namespace inovasyposmobile.Models.Persediaan
{
    public class ProdukStokModel
    {
        public string? ProdukStokId { get; set; }

        public string? TenantId { get; set; }

        [Required]
        public string? ProdukId { get; set; }

        [Required]
        public string? GudangId { get; set; }

        public DateTime TanggalMasuk { get; set; }

        public DateTime TanggalExpired { get; set; }

        public decimal HargaModal { get; set; }

        public decimal HargaJual { get; set; }

        public int Masuk { get; set; }

        public int Keluar { get; set; }

        public int Saldo { get; set; }

        public string? Varian { get; set; }
        public string? BatchNo { get; set; }
        public string? Deskripsi { get; set; }

        public string? RecordStatus { get; set; }

        public virtual ProdukModel? Produk { get; set; }

        public virtual GudangModel? Gudang { get; set; }

        public string? ProdukVarian
        {
            get
            {
                string? produkVarian = Produk?.Nama;

                if (!string.IsNullOrEmpty(Produk?.Warna))
                {
                    produkVarian += " " + Produk?.Warna;
                }

                if (!string.IsNullOrEmpty(Produk?.Ukuran))
                {
                    produkVarian += " " + Produk?.Ukuran;
                }

                if (!string.IsNullOrEmpty(Varian))
                {
                    produkVarian += " " + Varian;
                }

                return produkVarian;
            }
        }

        public string ProdukUkuranWarnaVarian
        {
            get
            {
                string produkVarian = "";

                if (!string.IsNullOrEmpty(Produk?.Warna))
                {
                    produkVarian += " " + Produk?.Warna;
                }

                if (!string.IsNullOrEmpty(Produk?.Ukuran))
                {
                    produkVarian += " " + Produk?.Ukuran;
                }

                if (!string.IsNullOrEmpty(Varian))
                {
                    produkVarian += " " + Varian;
                }

                return produkVarian;
            }
        }
    }
}