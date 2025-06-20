using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.ViewModels.Transaksi.Penjualan
{
    public class PenjualanCreateDetailViewModel : BaseViewModel
    {
        private string _produkId = "";
        public string ProdukId
        {
            get => _produkId;
            set => SetProperty(ref _produkId, value);
        }

        private string _produkStokId = "";
        public string ProdukStokId
        {
            get => _produkStokId;
            set => SetProperty(ref _produkStokId, value);
        }

        private decimal _jumlah;
        public decimal Jumlah
        {
            get => _jumlah;
            set => SetProperty(ref _jumlah, value);
        }

        private int _berat;
        public int Berat
        {
            get => _berat;
            set => SetProperty(ref _berat, value);
        }
    }
}