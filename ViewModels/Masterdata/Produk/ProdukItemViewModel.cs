using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inovasyposmobile.Models.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Produk
{
    public class ProdukItemViewModel : BaseViewModel
    {
        private ProdukModel? _produk;
        public ProdukModel? Produk
        {
            get => _produk;
            set => SetProperty(ref _produk, value);
        }

        private bool _hasImage = false;
        public bool HasImage
        {
            get => _hasImage;
            set
            {
                SetProperty(ref _hasImage, value);
                OnPropertyChanged(nameof(HasNoImage));
            }
        }

        public bool HasNoImage => !HasImage;
    }
}