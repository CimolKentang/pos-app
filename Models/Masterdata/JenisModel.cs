using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Masterdata
{
    public class JenisModel : BaseModel
    {
        public string? JenisId { get; set; }
        public string? TenantId { get; set; }
        public string? Nama { get; set; }
        public string? Kode { get; set; }
        public bool IsShowExpiredDate { get; set; }
        public bool IsShowVarian { get; set; }
        public bool IsShowUkuran { get; set; }
        public bool IsShowWarna { get; set; }
        public bool IsShowBerat { get; set; }
        public bool IsShowTinggi { get; set; }
        public bool IsShowPanjang { get; set; }
        public bool IsShowLebar { get; set; }
        public bool IsShowPomTR { get; set; }
        public bool IsShowKenaPajak { get; set; }
        public bool IsShowJualOnline { get; set; }
    }
}