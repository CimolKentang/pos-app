using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Requests;

namespace inovasyposmobile.Models.Akuntansi
{
    public class AkunSearchParams : SearchSortFilterPagingModel
    {
        public AkunSearchParams()
        {
            IsValueDisPlay = true;
            ExceptAkunIds = new List<string>();
        }
        
        public bool IsForPenjualan { get; set; }
        public bool IsForPembelian { get; set; }

        public bool IsForCashIn { get; set; }

        public bool IsForCashOut { get; set; }

        public List<ValueDisplayRequestModel>? FilterJenisAkun { get; set; }
        public List<ValueDisplayRequestModel>? FilterSubAkun { get; set; }

        public List<string> ExceptAkunIds { get; set; }
    }
}