using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;

namespace inovasyposmobile.Models.Konsinyasi
{
    public class KonsinyasiModel : BaseModel
    {
        public KonsinyasiModel()
        {
            KonsinyasiDetails = new List<KonsinyasiDetailModel>();
        }

        public string? KonsinyasiId { get; set; }

        public string? TenantId { get; set; }
        public string? NomorKonsinyasi { get; set; }

        public string? GudangId { get; set; }

        public string? NamaGudang { get; set; }

        public string? PelangganId { get; set; }

        public string? NamaPelanggan { get; set; }

        public string? SalesId { get; set; }

        public string? NamaSales { get; set; }

        public DateTime TanggalJoin { get; set; }

        public decimal Pajak { get; set; }

        public decimal SubTotal { get; set; }

        public string? StatusKonsinyasi { get; set; }

        public string? KeteranganCancel { get; set; }
        public virtual PelangganModel? Pelanggan { get; set; }

        public virtual KaryawanModel? Karyawan { get; set; }

        public virtual GudangModel? Gudang { get; set; }

        public virtual ICollection<KonsinyasiDetailModel> KonsinyasiDetails { get; set; }
    }
}