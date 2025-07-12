using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Extensions;
using inovasyposmobile.Models.Persediaan;

namespace inovasyposmobile.Models.Masterdata
{
    public class ProdukModel : BaseModel
    {
        public ProdukModel()
        {
            ProdukStoks = new List<ProdukStokModel>();
        }
        public string? ProdukId { get; set; }

        public string? TenantId { get; set; }

        [Required]
        public string? JenisId { get; set; }

        [Required]
        public string? MerekId { get; set; }

        [Required]
        public string? SatuanId { get; set; }

        [Required]
        public string? SupplierId { get; set; }

        [Required]
        public string? Kode { get; set; }

        public string? KodeRef { get; set; }

        [Required]
        public string? Nama { get; set; }

        public string? Gambar { get; set; }

        public string? GambarSmall { get; set; }

        public bool? IsGambarChanged { get; set; }

        public string? Ukuran { get; set; }

        public string? Warna { get; set; }

        public int? Berat { get; set; } //gram        

        public int? Tinggi { get; set; } //cm

        public int? Panjang { get; set; } //cm

        public int? Lebar { get; set; } //cm

        public bool? IsTaxable { get; set; }

        public bool? IsAllowOnline { get; set; }
        /// <summary>
        /// Stok Minimum
        /// </summary>
        /// <value></value>
        public int? MinStok { get; set; }

        /// <summary>
        /// Lama Penggunaan Produk in Day
        /// </summary>
        /// <value></value>
        public int? LamaPenggunaan { get; set; }
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public bool? UseMinStokAlert { get; set; }
        public string? Lokasi { get; set; }

        public int? TotalStok
        {
            get
            {
                int stok = 0;

                if (ProdukStoks != null && ProdukStoks.Any())
                    stok = ProdukStoks.Where(a => a != null).Sum(a => a.Saldo);

                return stok;
            }
        }

        public string? HargaJual
        {
            get
            {
                string hargaJual = "-";

                if (ProdukStoks != null && ProdukStoks.Any())
                {
                    List<ProdukStokModel> produkStoks = ProdukStoks
                    .Where(a => a != null && a.Saldo >= 0)
                    .OrderBy(a => a.HargaJual).ToList();

                    if (produkStoks.Count >= 1)
                        hargaJual = $"{produkStoks.First().HargaJual:N0}";

                    if (produkStoks.Count >= 2 && produkStoks.First().HargaJual != produkStoks.Last().HargaJual)
                        hargaJual = $"{hargaJual} - {produkStoks.Last().HargaJual:N0}";
                }

                return hargaJual.CommaToTitik();
            }
        }

        public string? HargaModal
        {
            get
            {
                string hargaModal = "-";

                if (ProdukStoks != null && ProdukStoks.Any())
                {
                    List<ProdukStokModel> produkStoks = ProdukStoks
                    .Where(a => a != null && a.Saldo >= 0)
                    .OrderBy(a => a.HargaModal).ToList();

                    if (produkStoks.Count >= 1)
                        hargaModal = $"{produkStoks.First().HargaModal:N0}";

                    if (produkStoks.Count >= 2 && produkStoks.First().HargaModal != produkStoks.Last().HargaModal)
                        hargaModal = $"{hargaModal} - {produkStoks.Last().HargaModal:N0}";
                }

                return hargaModal.CommaToTitik();
            }
        }

        public string? GudangId { get; set; }
        public string? NamaGudang { get; set; }

        public virtual JenisModel? Jenis { get; set; }

        public virtual MerekModel? Merek { get; set; }

        public virtual SatuanModel? Satuan { get; set; }

        public virtual SupplierModel? Supplier { get; set; }

        public virtual ICollection<ProdukStokModel>? ProdukStoks { get; set; }
    }
}