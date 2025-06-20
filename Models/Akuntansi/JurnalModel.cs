using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.Models.Akuntansi
{
    public class JurnalModel : BaseModel
    {
        public string? JurnalId { get; set; }
        public string? TenantId { get; set; }
        public string? AkunId { get; set; }

        public string? NamaAkun { get; set; }

        public DateTime Tanggal { get; set; }

        /// <summary>
        /// Urutan Record
        /// </summary>
        /// <value></value>
        public long OrderNumber { get; set; }

        /// <summary>
        /// Grouping 1 Traksaksi Jurnal
        /// </summary>
        /// <value></value>
        public long GroupJurnal { get; set; }
        public decimal Debit { get; set; }
        public decimal Kredit { get; set; }
        public decimal Saldo { get; set; }

        public string? ReferensiId { get; set; }

        public string? Referensi { get; set; }

        public bool IsHeader { get; set; }
        public virtual AkunModel? Akun { get; set; }
    }
}