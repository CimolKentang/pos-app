using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Requests;

namespace inovasyposmobile.Models.Masterdata
{
    public class ProdukSearchParams : SearchSortFilterPagingModel
    {
        public ProdukSearchParams()
        {
            KodeProduks = new List<string>();
            FilterGudang = new List<ValueDisplayRequestModel>();
            FilterJenis = new List<ValueDisplayRequestModel>();
            FilterMerek = new List<ValueDisplayRequestModel>();
            FilterSupplier = new List<ValueDisplayRequestModel>();
            FilterSatuan = new List<ValueDisplayRequestModel>();
        }
        public int MinStok { get; set; }
        public List<string> KodeProduks { get; set; }
        public List<ValueDisplayRequestModel> FilterGudang { get; set; }
        public List<ValueDisplayRequestModel> FilterJenis { get; set; }
        public List<ValueDisplayRequestModel> FilterMerek { get; set; }
        public List<ValueDisplayRequestModel> FilterSupplier { get; set; }
        public List<ValueDisplayRequestModel> FilterSatuan { get; set; }
    }
}