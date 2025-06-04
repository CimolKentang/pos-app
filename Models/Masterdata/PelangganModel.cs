using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class PelangganModel : BaseModel
    {
        public string? PelangganId { get; set; }

        public string? TenantId { get; set; }

        public string? GroupPelangganId { get; set; }

        public string? TipePelangganId { get; set; }

        public string? Kode { get; set; }

        public string? KodeRef { get; set; }

        public string? Nama { get; set; }

        public string? Alamat { get; set; }

        public string? WilayahId { get; set; }

        public string? DetailWilayah { get; set; }

        public string? KaryawanId { get; set; }

        public string? NamaSales { get; set; }

        public string? Telepon { get; set; }

        public string? NoHp { get; set; }

        public string? Telegram { get; set; }

        public string? Email { get; set; }

        public DateTime TanggalJoin { get; set; }

        public string? SumberAgen { get; set; }

        public string? NamaTokoOffline { get; set; }

        public string? NamaTokoOnline { get; set; }

        public string? LinkTokoOnline { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public string? AlamatLengkap
        {
            get
            {
                string? alamatLengkap = Alamat;

                if (Wilayah != null)
                {
                    alamatLengkap = $"{Alamat}, {Wilayah.Display}";
                }

                return alamatLengkap;
            }
        }

        public virtual WilayahModel? Wilayah { get; set; }
        public virtual KaryawanModel? Karyawan { get; set; }
        public virtual TipePelangganModel? TipePelanggan { get; set; }
        public virtual GroupPelangganModel? GroupPelanggan { get; set; }
    }
}