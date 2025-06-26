using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Akuntansi;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Interfaces.Masterdata;
using inovasyposmobile.Services.Interfaces.Transaksi;
using inovasyposmobile.ViewModels.Akuntansi;
using inovasyposmobile.ViewModels.Masterdata.Produk;
using inovasyposmobile.Views.Controls;

namespace inovasyposmobile.ViewModels.Transaksi.Penjualan
{
    public class PenjualanCreateViewModel : BaseViewModel
    {
        private readonly IPenjualanService _penjualanService;
        private readonly IProdukService _produkService;
        private readonly ValueDisplayViewModel _valueDisplayViewModel;

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

        // selected produks for display
        // penjualan details for sending the data to API
        private ObservableCollection<ProdukWithStokViewModel> _selectedProduks = new();
        public ObservableCollection<ProdukWithStokViewModel> SelectedProduks
        {
            get => _selectedProduks;
            set
            {
                SetProperty(ref _selectedProduks, value);
            }
        }

        private ObservableCollection<PenjualanDetailModel> _penjualanDetails = new();
        public ObservableCollection<PenjualanDetailModel> PenjualanDetails
        {
            get => _penjualanDetails;
            set => SetProperty(ref _penjualanDetails, value);
        }

        private ProdukSearchParams _produkSearchParams = new ProdukSearchParams();
        public ProdukSearchParams ProdukSearchParams
        {
            get => _produkSearchParams;
            set => SetProperty(ref _produkSearchParams, value);
        }

        #region for fetching produks
        private int _currentPage = 0;
        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private int _totalItem = 0;
        public int TotalItem
        {
            get => _totalItem;
            set => SetProperty(ref _totalItem, value);
        }

        private int _itemNumber = 0;
        public int ItemNumber
        {
            get => _itemNumber;
            set => SetProperty(ref _itemNumber, value);
        }

        private bool _hasNextPage = false;
        public bool HasNextPage
        {
            get => _hasNextPage;
            set => SetProperty(ref _hasNextPage, value);
        }

        private bool _isHandlingScroll = false;
        #endregion

        private bool _hasSelectedProduk = false;
        public bool HasSelectedProduk
        {
            get => _hasSelectedProduk;
            set => SetProperty(ref _hasSelectedProduk, value);
        }

        private string _deskripsi = "";
        public string Deskripsi
        {
            get => _deskripsi;
            set => SetProperty(ref _deskripsi, value);
        }

        #region payment
        private MetodePembayaranModel? _metodePembayaran;
        public MetodePembayaranModel? MetodePembayaran
        {
            get => _metodePembayaran;
            set => SetProperty(ref _metodePembayaran, value);
        }

        private ObservableCollection<JurnalViewModel> _jurnals = new();
        public ObservableCollection<JurnalViewModel> Jurnals
        {
            get => _jurnals;
            set
            {
                SetProperty(ref _jurnals, value);
            }
        }

        private bool _hasJurnal = false;
        public bool HasJurnal
        {
            get => _hasJurnal;
            set => SetProperty(ref _hasJurnal, value);
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

        private decimal _subTotal = 0;
        public decimal SubTotal
        {
            get => _subTotal;
            set => SetProperty(ref _subTotal, value);
        }

        private decimal _jumlahTotal = 0;
        public decimal JumlahTotal
        {
            get => _jumlahTotal;
            set => SetProperty(ref _jumlahTotal, value);
        }

        private decimal _minimalDP = 0;
        public decimal MinimalDP
        {
            get => _minimalDP;
            set
            {
                SetProperty(ref _minimalDP, value);
            }
        }

        private decimal _bayar = 0;
        public decimal Bayar
        {
            get => _bayar;
            set => SetProperty(ref _bayar, value);
        }

        private decimal _jumlahBayar = 0;
        public decimal JumlahBayar
        {
            get => _jumlahBayar;
            set
            {
                SetProperty(ref _jumlahBayar, value);
                HitungKembalian();
            }
        }

        private bool _autoJumlahBayar = false;
        public bool AutoJumlahBayar
        {
            get => _autoJumlahBayar;
            set => SetProperty(ref _autoJumlahBayar, value);
        }

        private decimal _sisaBayar = 0;
        public decimal SisaBayar
        {
            get => _sisaBayar;
            set => SetProperty(ref _sisaBayar, value);
        }

        private decimal _kembalian = 0;
        public decimal Kembalian
        {
            get => _kembalian;
            set => SetProperty(ref _kembalian, value);
        }

        private decimal _piutang = 0;
        public decimal Piutang
        {
            get => _piutang;
            set => SetProperty(ref _piutang, value);
        }
        #endregion

        public ICommand InitPenjualanKasirCommand { get; }
        public ICommand ProcessPenjualanKasirCommand { get; }
        public ICommand SelectAkunDialogCommand { get; }
        public ICommand SubmitPenjualanCommand { get; }
        public ICommand RefreshPenjualanKasirCommand { get; }
        public ICommand HandleScrollCommand { get; }

        public PenjualanCreateViewModel(
            IPenjualanService penjualanService,
            IProdukService produkService,
            ValueDisplayViewModel valueDisplayViewModel
        )
        {
            _penjualanService = penjualanService;
            _produkService = produkService;
            _valueDisplayViewModel = valueDisplayViewModel;

            MetodePembayaran = new MetodePembayaranModel();

            InitPenjualanKasirCommand = new Command(async () => await InitPenjualanKasir());
            RefreshPenjualanKasirCommand = new Command(async () => await RefreshPenjualanKasir());
            SelectAkunDialogCommand = new Command(async () => await SelectAkunDialog());
            ProcessPenjualanKasirCommand = new Command(ProcessPenjualanKasir);
            SubmitPenjualanCommand = new Command(async () => await SubmitPenjualan());
            HandleScrollCommand = new Command(async () => await HandleScroll());
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

                    await GetProduksWithStok();
                }
            }
            catch (InternetException ex)
            {
                await Toast.Make(ex.Message, ToastDuration.Short).Show();
            }
            catch (ApiException ex)
            {
                await Toast.Make(ex.Message, ToastDuration.Short).Show();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task GetProduksWithStok()
        {
            ProdukSearchParams.PageIndex = CurrentPage;
            var produks = await _produkService.GetProdukWithStoks(ProdukSearchParams);

            if (produks?.Data?.Items != null)
            {
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

                HasNextPage = produks.Data.HasNextPage;
                TotalItem = produks.Data.TotalItem;
                ItemNumber = Produks.Count;

                SubscribeToAllProducts();
            }
        }

        private async Task HandleScroll()
        {
            if (_isHandlingScroll == true) return;

            if (HasNextPage == true)
            {
                _isHandlingScroll = true;
                IsLoading = true;

                CurrentPage += 1;
                await GetProduksWithStok();

                _isHandlingScroll = false;
                IsLoading = false;
            }
        }

        private async Task RefreshPenjualanKasir()
        {
            IsRefreshing = true;

            ClearData();
            await InitPenjualanKasir();

            IsRefreshing = false;
        }

        private void ProcessPenjualanKasir()
        {
            PenjualanDetails.Clear();
            SelectedProduks.Clear();

            foreach (var produk in Produks)
            {
                if (produk.IsSelected == true)
                {
                    var penjualanDetail = new PenjualanDetailModel
                    {
                        TenantId = produk.ProdukWithStok.TenantId,
                        ProdukId = produk.ProdukWithStok.ProdukId,
                        ProdukStokId = produk.ProdukWithStok.ProdukStokId,
                        NamaProduk = produk.ProdukWithStok.NamaProduk,
                        ProdukVarian = produk.ProdukWithStok.ProdukVarian,
                        DeskripsiProduk = produk.ProdukWithStok.DeskripsiProduk,
                        JenisProduk = produk.ProdukWithStok.JenisProduk,
                        MerekProduk = produk.ProdukWithStok.MerekProduk,
                        SatuanProduk = produk.ProdukWithStok.SatuanProduk,
                        SupplierProduk = produk.ProdukWithStok.SupplierProduk,
                        HargaModal = produk.ProdukWithStok.HargaModal,
                        HargaJual = produk.ProdukWithStok.HargaJual,
                        Jumlah = produk.Count,
                        Berat = produk.ProdukWithStok.Berat,
                        Gambar = produk.ProdukWithStok.Gambar,
                        GambarSmall = produk.ProdukWithStok.GambarSmall,
                        IsDiskonProductAmount = true,
                        IsDiskonProductPercent = false,
                        RecordStatus = RecordStatusConstant.Active
                    };

                    PenjualanDetails.Add(penjualanDetail);
                    SelectedProduks.Add(produk);
                }
            }

            HitungSubTotals();
        }

        private async Task SelectAkunDialog()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectMultipleOneDialog(_valueDisplayViewModel, SelectMultipleForConstant.AkunPenjualan));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                var result = (ValueDisplayFilterModel)answer;

                var existingJurnal = Jurnals.Where(x => x.Jurnal.AkunId!.Equals(result.Value)).FirstOrDefault();

                if (existingJurnal == null)
                {
                    var filter = new ValueDisplayRequestModel
                    {
                        FilterDisplay = result.Display,
                        FilterId = result.Value
                    };

                    var jurnal = new JurnalModel
                    {
                        AkunId = filter.FilterId,
                        NamaAkun = filter.FilterDisplay,
                        Debit = HitungSisaBayar(), // update !!
                        RecordStatus = RecordStatusConstant.Active
                    };

                    var jurnalViewModel = new JurnalViewModel();
                    jurnalViewModel.Jurnal = jurnal;
                    jurnalViewModel.Debit = jurnal.Debit;
                    Jurnals.Add(jurnalViewModel);

                    HitungTotalDebitAkun();
                    SubscribeToAllJurnals();
                }

                if (Jurnals.Count > 0)
                {
                    HasJurnal = true;
                }
                else
                {
                    HasJurnal = false;
                }
            }
        }

        // update jumlah produk
        // reset ongkir
        // hitung akumulasi harga grosir

        private void UpdateMinimalDP()
        {
            var total = Total;
            MinimalDP = total;

            if (MetodePembayaran?.IsNeedDP == true)
            {
                decimal minimalDP = 0;
                decimal minDPByPersentaseDP = 0;

                if (MetodePembayaran.MinPersentaseDP > 0)
                {
                    var persen = MetodePembayaran.MinPersentaseDP / 100;
                    minDPByPersentaseDP = total * persen;
                    minimalDP = minDPByPersentaseDP;
                }

                if (minDPByPersentaseDP < MetodePembayaran.MinNominalDP)
                {
                    if (total > MetodePembayaran.MinNominalDP)
                    {
                        minimalDP = MetodePembayaran.MinNominalDP;
                    }
                    else
                    {
                        minimalDP = total;
                    }
                }

                MinimalDP = minimalDP;
            }
        }

        private void HitungSubTotals()
        {
            foreach (var item in PenjualanDetails)
            {
                var jumlah = item.Jumlah;
                var diskonPersentase = item.DiskonPersentase;
                var diskon = item.Diskon;
                var hargaJual = item.HargaJual;
                var subTotal = jumlah * hargaJual;

                if (item.IsDiskonProductAmount == true)
                {
                    diskonPersentase = diskon / hargaJual / jumlah * 100;
                    item.DiskonPersentase = diskonPersentase;
                }
                else
                {
                    diskon = subTotal * diskonPersentase / 100;
                    item.Diskon = diskon;
                }

                subTotal = subTotal - diskon;
                item.SubTotal = subTotal;
            }

            // hitung diskon penjualan
            HitungSubTotalPenjualan();
        }

        private void HitungSubTotalPenjualan()
        {
            decimal totalSubTotal = 0;
            decimal jumlahTotal = 0;
            int? beratPengiriman = 0;

            foreach (var item in PenjualanDetails)
            {
                totalSubTotal += item.SubTotal;
                jumlahTotal += item.Jumlah;
                beratPengiriman += item.Berat * item.Jumlah;
            }

            SubTotal = totalSubTotal;
            Penjualan!.BeratPengiriman = beratPengiriman;

            HitungTotalPenjualan();
            // hitung diskon penjualan
        }

        private void HitungTotalPenjualan()
        {
            var totalDiskon = Penjualan!.TotalDiskon ?? 0;
            var totalBiaya = Penjualan.TotalBiaya ?? 0;
            var subTotal = SubTotal;

            var total = subTotal + totalBiaya - totalDiskon;
            Total = total;
            Penjualan.Total = Total;

            UpdateMinimalDP();
            HitungTotalDebitAkun();
            HitungKembalian();
        }

        private void HitungTotalDebitAkun()
        {
            decimal totalDebitAkun = 0;
            foreach (var item in Jurnals)
            {
                totalDebitAkun += item.Debit;
            }

            Bayar = totalDebitAkun;

            if (AutoJumlahBayar)
            {
                JumlahBayar = totalDebitAkun;
            }

            // validators

            HitungSisaBayar();
            HitungKembalian();
        }

        private decimal HitungSisaBayar()
        {
            decimal totalDebitAkun = 0;
            foreach (var item in Jurnals)
            {
                totalDebitAkun += item.Debit;
            }

            decimal sisaBayar = Total - totalDebitAkun;
            SisaBayar = sisaBayar;

            return sisaBayar;
        }

        private void HitungKembalian()
        {
            if (JumlahBayar > Total)
            {
                if (Bayar == 0)
                {
                    Kembalian = 0;
                }
                else
                {
                    Kembalian = JumlahBayar - Bayar;
                }
            }
            else
            {
                Kembalian = 0;
            }
        }

        private void SubscribeToAllProducts()
        {
            if (Produks == null) return;
            foreach (var produk in Produks)
            {
                produk.PropertyChanged += OnChildPropertyChanged!;
            }
        }

        private void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // var produk = (ProdukWithStokViewModel)sender;

            // Console.WriteLine($"Child '{produk.ProdukWithStok!.NamaProduk}' changed property '{e.PropertyName}'");

            // if (e.PropertyName == nameof(ProdukWithStokViewModel.Count))
            // {
            //     Console.WriteLine($"Count updated to {produk.Count}");
            // }

            SubTotal = 0;
            JumlahTotal = 0;
            HasSelectedProduk = false;
            foreach (var item in Produks)
            {
                if (item.IsSelected)
                {
                    HasSelectedProduk = true;
                    var subTotal = item.ProdukWithStok.HargaJual * item.Count;

                    JumlahTotal += item.Count;
                    SubTotal += subTotal;

                    UpdateMinimalDP();
                }
            }
        }

        private void SubscribeToAllJurnals()
        {
            if (Jurnals == null) return;
            foreach (var item in Jurnals)
            {
                item.PropertyChanged += OnJurnalPropertyChanged!;
            }
        }

        private void OnJurnalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            JurnalViewModel? jurnal;
            if (e.PropertyName == nameof(jurnal.Debit))
            {
                HitungTotalDebitAkun();
            }
        }

        private async Task SubmitPenjualan()
        {
            try
            {
                var penjualan = new PenjualanModel
                {
                    PenjualanId = Penjualan!.PenjualanId,
                    TenantId = Penjualan.TenantId,
                    NomorPenjualan = Penjualan.NomorPenjualan,
                    GudangId = Penjualan.GudangId,
                    NamaGudang = Penjualan.NamaGudang,
                    PelangganId = Penjualan.PelangganId,
                    NamaPelanggan = Penjualan.NamaPelanggan,
                    SalesId = Penjualan.SalesId,
                    NamaSales = Penjualan.NamaSales,
                    Tanggal = Penjualan.Tanggal,
                    MetodePembayaranId = Penjualan.MetodePembayaranId,
                    NamaMetodePembayaran = Penjualan.NamaMetodePembayaran,
                    TanggalPesanan = Penjualan.TanggalPesanan,
                    JatuhTempo = Penjualan.JatuhTempo,
                    WilayahId = Penjualan.WilayahId,
                    WilayahPengiriman = Penjualan.WilayahPengiriman,
                    AlamatPengiriman = Penjualan.AlamatPengiriman,
                    Pajak = Penjualan.Pajak,
                    SubTotal = SubTotal,
                    BeratPengiriman = 0, //?

                    TotalDiskon = 0,
                    TotalBiaya = 0,
                    Total = Total,
                    SisaBayar = SisaBayar,
                    JumlahBayar = JumlahBayar,
                    Kembalian = Kembalian,
                    StatusPenjualan = Penjualan.StatusPenjualan,
                    StatusPembayaran = Penjualan.StatusPembayaran,
                    StatusPengiriman = Penjualan.StatusPengiriman,
                    ResiPengiriman = Penjualan.ResiPengiriman,
                    PenjualanDetails = PenjualanDetails.ToList(),
                    PenjualanDiskons = new List<PenjualanDiskonModel>(),
                    PenjualanBiayas = new List<PenjualanBiayaModel>(),
                    IsPenjualanKasir = Penjualan.IsPenjualanKasir,
                    SaveAsPesanan = Penjualan.SaveAsPesanan,
                    SaveAsCompleted = true,
                    Bayar = Bayar,
                    ListAkunPembayaran = Jurnals.Select(x => x.Jurnal).ToList(),
                    MinimalDP = MinimalDP,
                    SalesChannelId = Penjualan.SalesChannelId,
                    NamaSalesChannel = Penjualan.NamaSalesChannel,
                    SalesChannelReferensi = Penjualan.SalesChannelReferensi,
                    KurirId = Penjualan.KurirId,
                    NamaKurir = Penjualan.NamaKurir,
                    Deskripsi = Deskripsi,
                    RecordStatus = Penjualan.RecordStatus,
                    IsPickup = Penjualan.IsPickup,
                    PickupTime = Penjualan.PickupTime,
                    IsProsesAggregator = Penjualan.IsProsesAggregator,
                    OngkosKirim = Penjualan.OngkosKirim
                };

                var response = await _penjualanService.CreateAsync(penjualan);

                if (response?.Succeeded == true)
                {
                    ClearData();
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ClearData()
        {
            SelectedProduks.Clear();
            PenjualanDetails.Clear();
            Produks.Clear();
            Jurnals.Clear();
            CurrentPage = 0;
            HasNextPage = false;
            ItemNumber = 0;
            TotalItem = 0;
            HasSelectedProduk = false;
        }
    }
}