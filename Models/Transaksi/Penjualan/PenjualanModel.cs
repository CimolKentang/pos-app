using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Akuntansi;
using inovasyposmobile.Models.Konsinyasi;
using inovasyposmobile.Models.Masterdata;

namespace inovasyposmobile.Models.Transaksi.Penjualan
{
    public class PenjualanModel : BaseModel
    {
        public string? PenjualanId { get; set; }

        public string? TenantId { get; set; }

        public string? NomorPenjualan { get; set; }

        public string? GudangId { get; set; }

        public string? NamaGudang { get; set; }

        public string? PelangganId { get; set; }

        public string? NamaPelanggan { get; set; }

        public string? SalesId { get; set; }

        public string? KurirId { get; set; }

        public string? NamaSales { get; set; }

        public DateTime Tanggal { get; set; }
        public DateTime TanggalPesanan { get; set; }
        /// <summary>
        /// Tanggal Pelunasan Pembayaran
        /// </summary>
        /// <value></value>
        public DateTime? TanggalPelunasan { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        /// <value></value>
        public string? DikemasOleh { get; set; }
        public string? DikirimOleh { get; set; }
        public string? DicompletedOleh { get; set; }
        public string? DicancelOleh { get; set; }
        public DateTime? TanggalDikemas { get; set; }
        public DateTime? TanggalDikirim { get; set; }
        public DateTime? TanggalCompleted { get; set; }

        public bool IsProsesAggregator { get; set; }

        public string? MetodePembayaranId { get; set; }

        public string? NamaMetodePembayaran { get; set; }

        public DateTime? JatuhTempo { get; set; }

        public string? WilayahId { get; set; }

        public string? WilayahPengiriman { get; set; }

        public string? AlamatPengiriman { get; set; }

        public bool IncludePajak { get; set; }

        public decimal Pajak { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? TotalDiskonPersentase { get; set; }

        public decimal? TotalDiskon { get; set; }

        public decimal? TotalBiaya { get; set; }

        public decimal? Total { get; set; }

        public decimal JumlahBayar { get; set; }

        public decimal Kembalian { get; set; }

        public decimal? SisaBayar { get; set; }
        public decimal? Bayar { get; set; }

        public string? DeskripsiPembayaran { get; set; }

        public decimal? MinimalDP { get; set; }

        public string? StatusPenjualan { get; set; }

        public string? StatusPembayaran { get; set; }

        public string? StatusPengiriman { get; set; }
        public string? StatusPenjualanBeforeCancel { get; set; }
        public string? StatusPembayaranBeforeCancel { get; set; }

        public string? ResiPengiriman { get; set; }

        public bool IsBisaDikirim { get; set; }
        public string? NamaKurir { get; set; }
        public string? KeteranganCancel { get; set; }
        public string? KeteranganPengiriman { get; set; }
        public string? KeteranganRetur { get; set; }

        /// <summary>
        /// User yang memperbolehkan pengiriman
        /// </summary>
        /// <value></value>
        public string? BisaDikirimOleh { get; set; }
        public string? MulaiDikemasOleh { get; set; }
        public string? KonsinyasiId { get; set; }

        public string? SalesChannelId { get; set; }

        /// <summary>
        /// Penjualan Dari Tokopedia, Shopee, Lazada, dll
        /// </summary>
        /// <value></value>
        public string? NamaSalesChannel { get; set; }

        /// <summary>
        /// Nomor Invoice Tokopedia, Shopee, Lazada, dll
        /// </summary>
        /// <value></value>
        public string? SalesChannelReferensi { get; set; }
        public bool IsCod { get; set; }

        /// <summary>
        /// In Gram
        /// </summary>
        /// <value></value>
        public int? BeratPengiriman { get; set; }

        public decimal OngkosKirim { get; set; }

        /// <summary>
        /// Nama Service Kurir
        /// </summary>
        /// <value></value>
        public string? KurirService { get; set; }
        public bool IsPickup { get; set; }
        public DateTime? PickupTime { get; set; }

        public DateTime? Estimation { get; set; }

        public bool IsInsurance { get; set; }

        public decimal? InsuranceFee { get; set; }

        public bool IsPenjualanKasir { get; set; }
        public bool IsPenjualanKonsinyasi { get; set; }
        public bool SaveAsPesanan { get; set; }
        public bool SaveAsCompleted { get; set; }
        public bool SaveAsSedangDikemas { get; set; }
        public int PrintPesananCount { get; set; }

        public int PrintAlamatCount { get; set; }

        public List<PenjualanDetailModel>? PenjualanDetails { get; set; }

        public List<PenjualanDiskonModel>? PenjualanDiskons { get; set; }

        public List<PenjualanBiayaModel>? PenjualanBiayas { get; set; }

        //for Penjualan Kasir
        public List<JurnalModel>? ListAkunPembayaran { get; set; }

        public MetodePembayaranModel? MetodePembayaran { get; set; }

        public WilayahModel? Wilayah { get; set; }

        public KurirModel? Kurir { get; set; }

        public SalesChannelModel? SalesChannel { get; set; }

        public PelangganModel? Pelanggan { get; set; }

        public KaryawanModel? Karyawan { get; set; }

        public KonsinyasiModel? Konsinyasi { get; set; }

        public int TotalJumlah
        {
            get
            {
                int totalJumlah = 0;

                if (PenjualanDetails != null && PenjualanDetails.Any())
                    totalJumlah = PenjualanDetails.Sum(a => a.Jumlah);

                return totalJumlah;
            }
        }

        public decimal TotalDiskonProduk
        {
            get
            {
                decimal totalDiskonProduk = 0;

                if (PenjualanDetails != null && PenjualanDetails.Any())
                    totalDiskonProduk = PenjualanDetails.Sum(a => a.Diskon);

                return totalDiskonProduk;
            }
        }

        public string? AlamatPengirimanLengkap
        {
            get
            {
                string? alamatLengkap = AlamatPengiriman;

                if (Wilayah != null)
                {
                    alamatLengkap = $"{AlamatPengiriman}, {Wilayah.Display}";
                }

                return alamatLengkap;
            }
        }
    }
}