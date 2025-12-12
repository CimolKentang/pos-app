using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Models.Persediaan;
using inovasyposmobile.Services.Implementations.Masterdata;
using inovasyposmobile.Views.Controls;

namespace inovasyposmobile.ViewModels.Masterdata.Produk
{
    public class ProdukCreateViewModel : BaseViewModel
    {
        private readonly ProdukService _produkService;
        private readonly ValueDisplayViewModel _valueDisplayViewModel;

        private ProdukModel? _produk;
        public ProdukModel? Produk
        {
            get => _produk;
            set => SetProperty(ref _produk, value);
        }

        private ObservableCollection<ProdukStokCreateViewModel> _stoks = new();
        public ObservableCollection<ProdukStokCreateViewModel> Stoks
        {
            get => _stoks;
            set => SetProperty(ref _stoks, value);
        }

        private bool _hasStok = false;
        public bool HasStok
        {
            get => _hasStok;
            set
            {
                SetProperty(ref _hasStok, value);
                OnPropertyChanged(nameof(HasNoStok));
            }
        }

        public bool HasNoStok => !HasStok;

        private string _nama = "";
        public string Nama
        {
            get => _nama;
            set => SetProperty(ref _nama, value);
        }

        private string _deskripsi = "";
        public string Deskripsi
        {
            get => _deskripsi;
            set => SetProperty(ref _deskripsi, value);
        }
        
        private string _barcode = "";
        public string Barcode
        {
            get => _barcode;
            set => SetProperty(ref _barcode, value);
        }

        private string _berat = "";
        public string Berat
        {
            get => _berat;
            set => SetProperty(ref _berat, value);
        }

        private string _lamaPenggunaan = "";
        public string LamaPenggunaan
        {
            get => _lamaPenggunaan;
            set => SetProperty(ref _lamaPenggunaan, value);
        }

        private string _lebar = "";
        public string Lebar
        {
            get => _lebar;
            set => SetProperty(ref _lebar, value);
        }

        private string _lokasi = "";
        public string Lokasi
        {
            get => _lokasi;
            set => SetProperty(ref _lokasi, value);
        }

        private string _minStok = "";
        public string MinStok
        {
            get => _minStok;
            set => SetProperty(ref _minStok, value);
        }

        private string _panjang = "";
        public string Panjang
        {
            get => _panjang;
            set => SetProperty(ref _panjang, value);
        }

        private string _tinggi = "";
        public string Tinggi
        {
            get => _tinggi;
            set => SetProperty(ref _tinggi, value);
        }

        private string _ukuran = "";
        public string Ukuran
        {
            get => _ukuran;
            set => SetProperty(ref _ukuran, value);
        }

        private string _warna = "";
        public string Warna
        {
            get => _warna;
            set => SetProperty(ref _warna, value);
        }

        private string _sku = "";
        public string Sku
        {
            get => _sku;
            set => SetProperty(ref _sku, value);
        }

        private string _useMinStokAlert = "";
        public string UseMinStokAlert
        {
            get => _useMinStokAlert;
            set => SetProperty(ref _useMinStokAlert, value);
        }

        private ValueDisplayFilterModel? _satuan;
        public ValueDisplayFilterModel? Satuan
        {
            get => _satuan;
            set => SetProperty(ref _satuan, value);
        }

        private ValueDisplayFilterModel? _jenis;
        public ValueDisplayFilterModel? Jenis
        {
            get => _jenis;
            set => SetProperty(ref _jenis, value);
        }

        private ValueDisplayFilterModel? _merek;
        public ValueDisplayFilterModel? Merek
        {
            get => _merek;
            set => SetProperty(ref _merek, value);
        }

        private ValueDisplayFilterModel? _supplier;
        public ValueDisplayFilterModel? Supplier
        {
            get => _supplier;
            set => SetProperty(ref _supplier, value);
        }

        private ValueDisplayFilterModel? _gudang;
        public ValueDisplayFilterModel? Gudang
        {
            get => _gudang;
            set => SetProperty(ref _gudang, value);
        }

        private ImageSource? _image;
        public ImageSource? Image
        {
            get => _image;
            set
            {
                SetProperty(ref _image, value);

                if (value != null)
                {
                    HasImage = true;
                }
                else
                {
                    HasImage = false;
                }
            }
        }

        private string? ImagePath { get; set; }

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

        public ICommand ClearDataCommand { get; }
        public ICommand InitDataCommand { get; }
        public ICommand AddDataCommand { get; }
        public ICommand OnSelectSatuanCommand { get; }
        public ICommand OnSelectJenisCommand { get; }
        public ICommand OnSelectMerekCommand { get; }
        public ICommand OnSelectSupplierCommand { get; }
        public ICommand OnSelectGudangCommand { get; }
        public ICommand PickPhotoCommand { get; }
        public ICommand CapturePhotoCommand { get; }

        public ProdukCreateViewModel(ProdukService ProdukService, ValueDisplayViewModel valueDisplayViewModel)
        {
            _produkService = ProdukService;
            _valueDisplayViewModel = valueDisplayViewModel;

            InitDataCommand = new Command(async () => await InitProduk());
            AddDataCommand = new Command(async () => await AddProduk());
            OnSelectSatuanCommand = new Command(async () => await OnSelectSatuan());
            OnSelectJenisCommand = new Command(async () => await OnSelectJenis());
            OnSelectMerekCommand = new Command(async () => await OnSelectMerek());
            OnSelectSupplierCommand = new Command(async () => await OnSelectSupplier());
            OnSelectGudangCommand = new Command(async () => await OnSelectGudang());
            PickPhotoCommand = new Command(async () => await PickPhoto());
            CapturePhotoCommand = new Command(async () => await CapturePhoto());
            ClearDataCommand = new Command(ClearData);
        }

        private async Task InitProduk()
        {
            if (Produk == null)
            {
                IsLoading = true;

                try
                {
                    var response = await _produkService.InitProduk();

                    if (response != null && response.Succeeded == true)
                    {
                        Produk = response.Data;

                        var stok = new ProdukStokCreateViewModel();
                        stok.GudangId = Produk!.GudangId!;
                        stok.NamaGudang = Produk.NamaGudang!;

                        Stoks.Add(stok);

                        if (Stoks.Count > 0)
                        {
                            HasStok = true;
                        }
                        else
                        {
                            HasStok = false;
                        }
                    }
                }
                catch (InternetException ex)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make(ex.Message, ToastDuration.Short).Show();
                }
                catch (ApiException ex)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make(ex.Message, ToastDuration.Short).Show();
                }
                finally
                {
                    IsLoading = false;
                }   
            }
        }

        private async Task AddProduk()
        {
            try
            {
                Produk!.Nama = Nama;
                Produk.Deskripsi = Deskripsi;
                Produk.Barcode = Barcode;
                Produk.Berat = string.IsNullOrEmpty(Berat) ? 0 : int.Parse(Berat);
                Produk.LamaPenggunaan = string.IsNullOrEmpty(LamaPenggunaan) ? null : int.Parse(LamaPenggunaan);
                Produk.Lebar = string.IsNullOrEmpty(Lebar) ? 0 : int.Parse(Lebar);
                Produk.Lokasi = Lokasi;
                Produk.MinStok = string.IsNullOrEmpty(MinStok) ? null : int.Parse(MinStok);
                Produk.Panjang = string.IsNullOrEmpty(Panjang) ? 0 : int.Parse(Panjang);
                Produk.Sku = Sku;
                Produk.Tinggi = string.IsNullOrEmpty(Tinggi) ? 0 : int.Parse(Tinggi);
                Produk.Ukuran = Ukuran;
                Produk.Warna = Warna;

                if (Satuan != null)
                {
                    Produk.SatuanId = Satuan.Value;   
                }

                if (Jenis != null)
                {
                    Produk.JenisId = Jenis.Value;
                }

                if (Merek != null)
                {
                    Produk.MerekId = Merek.Value;
                }

                if (Supplier != null)
                {
                    Produk.SupplierId = Supplier.Value;
                }

                // convert the image
                if (Image != null && ImagePath != null)
                {
                    byte[] imageBytes = await File.ReadAllBytesAsync(ImagePath);
                    string base64image = Convert.ToBase64String(imageBytes);
                    Produk.Gambar = base64image;
                }

                Produk.ProdukStoks = new List<ProdukStokModel>();
                foreach (var stok in Stoks)
                {
                    var stokItem = new ProdukStokModel();
                    stokItem.GudangId = stok.GudangId;
                    stokItem.HargaJual = string.IsNullOrEmpty(stok.HargaJual) ? 0 : int.Parse(stok.HargaJual);
                    stokItem.HargaModal = string.IsNullOrEmpty(stok.HargaModal) ? 0 : int.Parse(stok.HargaModal);
                    stokItem.RecordStatus = RecordStatusConstant.Active;
                    stokItem.Masuk = string.IsNullOrEmpty(stok.Masuk) ? 0 : int.Parse(stok.Masuk);
                    stokItem.Saldo = string.IsNullOrEmpty(stok.Saldo) ? 0 : int.Parse(stok.Saldo);
                    stokItem.TanggalMasuk = stok.TanggalMasuk;
                    stokItem.TanggalExpired = stok.TanggalExpired;
                    stokItem.Varian = stok.Varian;

                    Produk.ProdukStoks.Add(stokItem);
                }

                var response = await _produkService.CreateAsync(Produk);

                if (response != null && response.Succeeded == true)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make("Produk berhasil ditambah", ToastDuration.Short).Show();
                }
                else if (response != null && response.Succeeded == false)
                {
                    throw new ApiException(response.Messages![0]);
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
        }

        private async Task PickPhoto()
        {
            try
            {
                FileResult? fileResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a photo"
                });

                if (fileResult != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, fileResult.FileName);

                    using Stream sourceStream = await fileResult.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);

                    ImagePath = localFilePath;
                    Image = ImageSource.FromFile(localFilePath);
                }
            }
            catch (Exception e)
            {
                await Toast.Make(e.Message, ToastDuration.Short).Show();
            }
        }

        private async Task CapturePhoto()
        {
            try
            {
                FileResult? fileResult = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Pick a photo"
                });

                if (fileResult != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, fileResult.FileName);

                    using Stream sourceStream = await fileResult.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);

                    ImagePath = localFilePath;
                    Image = ImageSource.FromFile(localFilePath);
                }
            }
            catch (Exception e)
            {
                await Toast.Make(e.Message, ToastDuration.Short).Show();
            }
        }

        private async Task OnSelectSatuan()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectListOption(_valueDisplayViewModel, SelectMultipleForConstant.Satuan, "Satuan"));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                Satuan = (ValueDisplayFilterModel)answer;
            }
        }

        private async Task OnSelectJenis()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectListOption(_valueDisplayViewModel, SelectMultipleForConstant.Jenis, "Jenis"));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                Jenis = (ValueDisplayFilterModel)answer;
            }
        }
        
        private async Task OnSelectMerek()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectListOption(_valueDisplayViewModel, SelectMultipleForConstant.Merek, "Merek"));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                Merek = (ValueDisplayFilterModel)answer;
            }
        }

        private async Task OnSelectSupplier()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectListOption(_valueDisplayViewModel, SelectMultipleForConstant.Supplier, "Supplier"));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                Supplier = (ValueDisplayFilterModel)answer;
            }
        }

        private async Task OnSelectGudang()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectListOption(_valueDisplayViewModel, SelectMultipleForConstant.Gudang, "Gudang"));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                var gudang = (ValueDisplayFilterModel)answer;
                var existingGudang = Stoks.Where(s => s.GudangId.Equals(gudang.Value!)).FirstOrDefault();

                if (existingGudang != null)
                {
                    await Toast.Make($"Gudang {gudang.Display} sudah dipilih").Show();
                    return;
                }

                var stok = new ProdukStokCreateViewModel();
                stok.GudangId = gudang.Value!;
                stok.NamaGudang = gudang.Display!;

                Stoks.Add(stok);

                if (Stoks.Count > 0)
                {
                    HasStok = true;
                }
                else
                {
                    HasStok = false;
                }
            }
        }

        private void ClearData()
        {
            Stoks.Clear();
            Produk = null; Satuan = null; Merek = null; Jenis = null; Supplier = null;
            Nama = ""; Deskripsi = ""; Barcode = ""; Berat = ""; LamaPenggunaan = "";
            Lebar = ""; Lokasi = ""; MinStok = ""; Panjang = ""; Sku = "";
            Tinggi = ""; Ukuran = ""; Warna = "";
            Image = null; HasImage = false;
        }
    }
}