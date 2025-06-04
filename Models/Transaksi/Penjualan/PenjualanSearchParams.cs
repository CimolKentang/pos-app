using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Requests;

namespace inovasyposmobile.Models.Transaksi.Penjualan
{
    public class PenjualanSearchParams : SearchSortFilterPagingModel
    {
        public PenjualanSearchParams()
        {
            FilterGudang = new List<ValueDisplayRequestModel>();
            FilterStatusPenjualan = new List<ValueDisplayRequestModel>();
            FilterStatusPembayaran = new List<ValueDisplayRequestModel>();
            FilterStatusPengiriman = new List<ValueDisplayRequestModel>();
            FilterPelanggan = new List<ValueDisplayRequestModel>();
            FilterGroupPelanggan = new List<ValueDisplayRequestModel>();
            FilterTipePelanggan = new List<ValueDisplayRequestModel>();
            FilterSales = new List<ValueDisplayRequestModel>();
            FilterSalesChannel = new List<ValueDisplayRequestModel>();
            FilterKurir = new List<ValueDisplayRequestModel>();
            FilterMetodePembayaran = new List<ValueDisplayRequestModel>();
            ExceptListPenjualanId = new List<string>();
            SelectedPrintPenjualanIds = new List<string>();
            SortBy = "tanggal";
        }

        public List<string> SelectedPrintPenjualanIds { get; set; }
        public List<string> ExceptListPenjualanId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<ValueDisplayRequestModel> FilterGudang { get; set; }
        public List<ValueDisplayRequestModel> FilterStatusPenjualan { get; set; }
        public List<ValueDisplayRequestModel> FilterStatusPembayaran { get; set; }
        public List<ValueDisplayRequestModel> FilterStatusPengiriman { get; set; }
        public List<ValueDisplayRequestModel> FilterPelanggan { get; set; }
        public List<ValueDisplayRequestModel> FilterGroupPelanggan { get; set; }
        public List<ValueDisplayRequestModel> FilterTipePelanggan { get; set; }
        public List<ValueDisplayRequestModel> FilterSales { get; set; }
        public List<ValueDisplayRequestModel> FilterSalesChannel { get; set; }
        public List<ValueDisplayRequestModel> FilterKurir { get; set; }
        public List<ValueDisplayRequestModel> FilterMetodePembayaran { get; set; }

        /// <summary>
        /// Reference FilterResi Contans
        /// </summary>
        /// <value></value>
        public string? FilterResi { get; set; }

        /// <summary>
        /// Reference FilterJenisPenjualan Contains
        /// </summary>
        /// <value></value>
        public string? FilterJenisPenjualan { get; set; }

        /// <summary>
        /// If Trus Filter Periode By Tanggal Pelunasan
        /// </summary>
        /// <value></value>
        public bool IsFilterPeriodePelunasan { get; set; }
    }
}