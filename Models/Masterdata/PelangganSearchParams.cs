using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Requests;

namespace inovasyposmobile.Models.Masterdata
{
    public class PelangganSearchParams : SearchSortFilterPagingModel
    {
        public PelangganSearchParams()
        {
            IsValueDisPlay = true;
            FilterSales = new List<ValueDisplayRequestModel>();
            FilterGroupPelanggan = new List<ValueDisplayRequestModel>();
            FilterTipePelanggan = new List<ValueDisplayRequestModel>();
        }

        public List<ValueDisplayRequestModel> FilterSales { get; set; }
        public List<ValueDisplayRequestModel> FilterGroupPelanggan { get; set; }
        public List<ValueDisplayRequestModel> FilterTipePelanggan { get; set; }
    }
}