using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class GudangModel : BaseModel
    {
        public string? GudangId { get; set; }
        public string? TenantId { get; set; }
        public string? WilayahId { get; set; }
        public string? Kode { get; set; }
        public string? Nama { get; set; }
        public string? Alamat { get; set; }
        public string? Telepon { get; set; }
        public string? Email { get; set; }
        public bool IsMain { get; set; }
        public bool IsKonsinyasi { get; set; }
        public string? PersonInCharge { get; set; }
        public virtual WilayahModel? Wilayah { get; set; }
        public string? AlamatLengkap { get; set; }
    }
}