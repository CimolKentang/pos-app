using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class SupplierModel : BaseModel
    {
        public string? SupplierId { get; set; }
        public string? TenantId { get; set; }
        public string? WilayahId { get; set; }
        public string? Kode { get; set; }
        public string? Nama { get; set; }
        public string? Alamat { get; set; }
        public string? Telepon { get; set; }
        public string? NoHp { get; set; }
        public string? KontakPerson { get; set; }
        public string? Email { get; set; }
        public string? NamaBank { get; set; }
        public string? NoRekening { get; set; }
        public string? NamaPemilik { get; set; }
        public string? AlamatLengkap { get; set; }
    }
}