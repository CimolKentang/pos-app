using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using inovasyposmobile.Models.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Produk
{
    public class ProdukWithStokViewModel : BaseViewModel
    {
        private ProdukWithStokModel _produkWithStok = new ProdukWithStokModel();
        public ProdukWithStokModel ProdukWithStok
        {
            get => _produkWithStok;
            set => SetProperty(ref _produkWithStok, value);
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private int _count = 0;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private decimal _total = 0;
        public decimal Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
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

        public ICommand SelectToggleCommand { get; }
        public ICommand IncreaseCountCommand { get; }
        public ICommand DecreaseCountCommand { get; }

        public ProdukWithStokViewModel()
        {
            SelectToggleCommand = new Command(SelectToggle);
            IncreaseCountCommand = new Command(IncreaseCount);
            DecreaseCountCommand = new Command(DecreaseCount);
        }

        private void SelectToggle()
        {
            IsSelected = !IsSelected;
            if (IsSelected == false)
            {
                Count = 0;
                Total = 0;
            }
            else
            {
                IncreaseCount();
            }
        }

        private void IncreaseCount()
        {
            if (Count < ProdukWithStok.Saldo)
            {
                Count += 1;

                Total = Count * ProdukWithStok.HargaJual;
            }
        }

        private void DecreaseCount()
        {
            if (Count > 0)
            {
                Count -= 1;   

                Total = Count * ProdukWithStok.HargaJual;
            }
        }
    }
}