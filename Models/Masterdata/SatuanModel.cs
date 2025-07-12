using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class SatuanModel : BaseModel
    {
        public string? SatuanId { get; set; }
        public string? TenantId { get; set; }
        public string? Nama { get; set; }
        public string? Kode { get; set; }
    }
}