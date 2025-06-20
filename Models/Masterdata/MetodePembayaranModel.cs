using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class MetodePembayaranModel : BaseModel
    {
        public string? MetodePembayaranId { get; set; } = "temmId";

        public string? TenantId { get; set; } = "tenantId";

        public string? Kode { get; set; }

        public string? Nama { get; set; }

        /// <summary>
        /// Notifikasi Ke Verifikator Jika ada pembayaran
        /// </summary>
        /// <value></value>
        public bool IsNeedVerifikasi { get; set; } = false;

        /// <summary>
        /// Verifikasi By Karyawan
        /// </summary>
        public string? KaryawanId { get; set; }

        /// <summary>
        /// untuk pembayaran COD
        /// </summary>
        /// <value></value>
        public bool IsCOD { get; set; } = false;

        /// <summary>
        /// Template DP Ketika Buat Invoice
        /// </summary>
        /// <value></value>
        public bool IsNeedDP { get; set; } = false;

        /// <summary>
        /// Persentase dari total transaksi
        /// </summary>
        public decimal MinPersentaseDP { get; set; } = 0;

        /// <summary>
        /// Isi Jika Min Persentase DP kosong
        /// </summary>
        public decimal MinNominalDP { get; set; } = 0;

        public bool IsTempo { get; set; } = false;
        public bool IsHarusLunasSebelumMulaiPengemasanPenjualan { get; set; } = false;
        public bool IsMulaiPengemasanPenjualanSetelahVerifikasi { get; set; } = false;
        public bool IsNeedBuktiPembayaranPenjualan { get; set; } = false;
        public bool IsNeedBuktiPembayaranPembelian { get; set; } = false;
        public bool IsNeedBuktiPembayaranReturPenjualan { get; set; } = false;
        public bool IsNeedBuktiPembayaranReturPembelian { get; set; } = false;

        /// <summary>
        /// Isi Jika IsTempo = true
        /// </summary>
        public int DayJatuhTempo { get; set; } = 0;

        public virtual KaryawanModel? Karyawan { get; set; }
    }
}