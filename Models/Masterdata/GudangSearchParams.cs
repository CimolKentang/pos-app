using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Requests;

namespace inovasyposmobile.Models.Masterdata
{
    public class GudangSearchParams : SearchSortFilterPagingModel
    {
        public GudangSearchParams()
        {
            ExceptGudangIds = new List<string>();
        }

        public bool IncludeGudangKonsinyasi { get; set; }
        public List<string> ExceptGudangIds { get; set; }
    }
}