using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Interfaces.Masterdata;
using inovasyposmobile.Services.Interfaces.Transaksi;
using inovasyposmobile.ViewModels.Masterdata.Produk;

namespace inovasyposmobile.ViewModels.Transaksi.Penjualan
{
    public class PenjualanCreateViewModel : BaseViewModel
    {
        private readonly IPenjualanService _penjualanService;
        private readonly IProdukService _produkService;

        private PenjualanModel? _penjualan;
        public PenjualanModel? Penjualan
        {
            get => _penjualan;
            set => SetProperty(ref _penjualan, value);
        }

        private ObservableCollection<ProdukWithStokViewModel> _produks = new();
        public ObservableCollection<ProdukWithStokViewModel> Produks
        {
            get => _produks;
            set
            {
                SetProperty(ref _produks, value);
            }
        }

        private ObservableCollection<ProdukWithStokViewModel> _selectedProduks = new();
        public ObservableCollection<ProdukWithStokViewModel> SelectedProduks
        {
            get => _selectedProduks;
            set
            {
                SetProperty(ref _selectedProduks, value);
            }
        }

        private decimal _total = 0;
        public decimal Total
        {
            get => _total;
            set
            {
                SetProperty(ref _total, value);
            }
        }

        private ProdukSearchParams _produkSearchParams = new ProdukSearchParams();
        public ProdukSearchParams ProdukSearchParams
        {
            get => _produkSearchParams;
            set => SetProperty(ref _produkSearchParams, value);
        }

        private bool _hasSelectedProduk = false;
        public bool HasSelectedProduk
        {
            get => _hasSelectedProduk;
            set => SetProperty(ref _hasSelectedProduk, value);
        }

        public ICommand InitPenjualanKasirCommand { get; }
        public ICommand ProcessPenjualanKasirCommand { get; }

        public PenjualanCreateViewModel(
            IPenjualanService penjualanService,
            IProdukService produkService
        )
        {
            _penjualanService = penjualanService;
            _produkService = produkService;

            InitPenjualanKasirCommand = new Command(async () => await InitPenjualanKasir());
            ProcessPenjualanKasirCommand = new Command(ProcessPenjualanKasir);
        }

        private async Task InitPenjualanKasir()
        {
            IsLoading = true;

            try
            {
                var penjualan = await _penjualanService.InitPenjualanKasir();

                if (penjualan != null && penjualan.Succeeded)
                {
                    Penjualan = penjualan.Data;

                    var filterGudang = new ValueDisplayRequestModel
                    {
                        FilterId = Penjualan!.GudangId,
                        FilterDisplay = Penjualan.NamaGudang
                    };

                    ProdukSearchParams.FilterGudang.Add(filterGudang);

                    ProdukSearchParams.MinStok = 1;
                    var produks = await _produkService.GetProdukWithStoks(ProdukSearchParams);

                    if (produks?.Data?.Items != null)
                    {
                        Produks.Clear();

                        foreach (var item in produks.Data.Items)
                        {
                            var produkViewModel = new ProdukWithStokViewModel();
                            produkViewModel.ProdukWithStok = item;

                            if (!string.IsNullOrEmpty(item.Gambar))
                            {
                                produkViewModel.HasImage = true;
                            }

                            Produks.Add(produkViewModel);
                        }

                        SubscribeToAllProducts();
                    }
                }
            }
            catch (InternetException ex)
            {
                await Toast.Make(ex.Message, ToastDuration.Short).Show();
                // await DialogService.ShowAlertAsync("Connection Error", ex.Message, "OK");
            }
            catch (ApiException ex)
            {
                await Toast.Make(ex.Message, ToastDuration.Short).Show();
                // await DialogService.ShowAlertAsync("API Error", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ProcessPenjualanKasir()
        {
            SelectedProduks.Clear();

            foreach (var produk in Produks)
            {
                if (produk.IsSelected == true)
                {
                    SelectedProduks.Add(produk);
                }
            }
        }

        private void SubscribeToAllProducts()
        {
            if (Produks == null) return;
            foreach (var produk in Produks)
            {
                produk.PropertyChanged += OnChildPropertyChanged;
            }
        }

        private void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var produk = (ProdukWithStokViewModel)sender;

            Console.WriteLine($"Child '{produk.ProdukWithStok!.NamaProduk}' changed property '{e.PropertyName}'");

            if (e.PropertyName == nameof(ProdukWithStokViewModel.Count))
            {
                Console.WriteLine($"Count updated to {produk.Count}");
            }

            Total = 0;
            HasSelectedProduk = false;
            foreach (var item in Produks)
            {
                if (item.IsSelected)
                {
                    HasSelectedProduk = true;
                    var subTotal = item.ProdukWithStok.HargaJual * item.Count;
                    Total += subTotal;
                }
            }
        }
    }
}