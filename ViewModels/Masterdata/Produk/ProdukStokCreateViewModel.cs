using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.ViewModels.Masterdata.Produk
{
    public class ProdukStokCreateViewModel : BaseViewModel
    {
        public string GudangId { get; set; } = "";

        private string _hargaJual = "";
        public string HargaJual
        {
            get => _hargaJual;
            set => SetProperty(ref _hargaJual, value);
        }

        private string _hargaModal = "";
        public string HargaModal
        {
            get => _hargaModal;
            set => SetProperty(ref _hargaModal, value);
        }

        private string _namaGudang = "";
        public string NamaGudang
        {
            get => _namaGudang;
            set => SetProperty(ref _namaGudang, value);
        }

        private string _varian = "";
        public string Varian
        {
            get => _varian;
            set => SetProperty(ref _varian, value);
        }

        private string _saldo = "";
        public string Saldo
        {
            get => _saldo;
            set
            {
                SetProperty(ref _saldo, value);
                Masuk = value;
            }
        }

        public string Masuk { get; set; } = "";

        private DateTime _tanggalExpired = DateTime.Now;
        public DateTime TanggalExpired
        {
            get => _tanggalExpired;
            set => SetProperty(ref _tanggalExpired, value);
        }

        private DateTime _tanggalMasuk = DateTime.Now;
        public DateTime TanggalMasuk
        {
            get => _tanggalMasuk;
            set => SetProperty(ref _tanggalMasuk, value);
        }
    }
}