using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Akuntansi
{
    public class AkunModel : BaseModel
    {
        public string? AkunId { get; set; }
        public string? TenantId { get; set; }
        public string? JenisAkunId { get; set; }
        public string? Nama { get; set; }
        public string? NomorAkun { get; set; }
        public string? ChildOfAkunId { get; set; }
        public string? MataUang { get; set; }
        public string? PajakId { get; set; }
        public string? NamaBank { get; set; }
        public string? NamaPemilik { get; set; }
        public string? NomorRekening { get; set; }
        public bool IsAkunBank { get; set; }
        public bool IsTopParent { get; set; }
        public bool IsSubParent { get; set; }
        public bool IsForPenjualan { get; set; }
        public bool IsForPembelian { get; set; }
        public bool IsForCashIn { get; set; }
        public bool IsForCashOut { get; set; }

        public decimal Debet { get; set; }
        public decimal Kredit { get; set; }
        public decimal Saldo { get; set; }

        public string? OldId { get; set; }

        public virtual AkunModel? ParentOf { get; set; }
        public virtual AkunModel? ChildOf { get; set; }

        // public virtual JenisAkunModel JenisAkun { get; set; }

        // public virtual PajakModel Pajak { get; set; }
    }
}