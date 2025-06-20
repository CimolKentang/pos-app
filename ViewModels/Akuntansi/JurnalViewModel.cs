using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Akuntansi;

namespace inovasyposmobile.ViewModels.Akuntansi
{
    public class JurnalViewModel : BaseViewModel
    {
        private JurnalModel _jurnal = new JurnalModel();
        public JurnalModel Jurnal
        {
            get => _jurnal;
            set => SetProperty(ref _jurnal, value);
        }

        private decimal _debit;
        public decimal Debit
        {
            get => _debit;
            set => SetProperty(ref _debit, value);
        }
    }
}