using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Implementations.Masterdata;
using inovasyposmobile.Views.Controls;

namespace inovasyposmobile.ViewModels.Masterdata.Supplier
{
    public class SupplierCreateViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;
        private readonly ValueDisplayViewModel _valueDisplayViewModel;

        private SupplierModel? _supplier;
        public SupplierModel? Supplier
        {
            get => _supplier;
            set => SetProperty(ref _supplier, value);
        }

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

        private string _alamat = "";
        public string Alamat
        {
            get => _alamat;
            set => SetProperty(ref _alamat, value);
        }

        private string _kontakPerson = "";
        public string KontakPerson
        {
            get => _kontakPerson;
            set => SetProperty(ref _kontakPerson, value);
        }

        private string _telepon = "";
        public string Telepon
        {
            get => _telepon;
            set => SetProperty(ref _telepon, value);
        }

        private string _noHP = "";
        public string NoHP
        {
            get => _noHP;
            set => SetProperty(ref _noHP, value);
        }

        private string _email = "";
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _namaBank = "";
        public string NamaBank
        {
            get => _namaBank;
            set => SetProperty(ref _namaBank, value);
        }

        private string _noRekening = "";
        public string NoRekening
        {
            get => _noRekening;
            set => SetProperty(ref _noRekening, value);
        }

        private string _namaPemilik = "";
        public string NamaPemilik
        {
            get => _namaPemilik;
            set => SetProperty(ref _namaPemilik, value);
        }

        private ValueDisplayFilterModel? _wilayah;
        public ValueDisplayFilterModel? Wilayah
        {
            get => _wilayah;
            set => SetProperty(ref _wilayah, value);
        }

        public ICommand ClearDataCommand { get; }
        public ICommand InitDataCommand { get; }
        public ICommand AddDataCommand { get; }
        public ICommand OnSelectWilayahCommand { get; }

        public SupplierCreateViewModel(SupplierService supplierService, ValueDisplayViewModel valueDisplayViewModel)
        {
            _supplierService = supplierService;
            _valueDisplayViewModel = valueDisplayViewModel;

            InitDataCommand = new Command(async () => await InitSupplier());
            AddDataCommand = new Command(async () => await AddSupplier());
            ClearDataCommand = new Command(ClearData);
            OnSelectWilayahCommand = new Command(async () => await OnSelectWilayah());
        }

        private async Task InitSupplier()
        {
            IsLoading = true;

            try
            {
                var response = await _supplierService.InitSupplier();

                if (response != null && response.Succeeded == true)
                {
                    Supplier = response.Data;
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

        private async Task AddSupplier()
        {
            try
            {
                Supplier!.Nama = Nama;
                Supplier.Deskripsi = Deskripsi;
                Supplier.Alamat = Alamat;
                Supplier.Email = Email;
                Supplier.KontakPerson = KontakPerson;
                Supplier.NamaBank = NamaBank;
                Supplier.NamaPemilik = NamaPemilik;
                Supplier.NoHp = NoHP;
                Supplier.NoRekening = NoRekening;
                Supplier.Telepon = Telepon;
                Supplier.WilayahId = Wilayah?.Value;

                var response = await _supplierService.CreateAsync(Supplier);

                if (response != null && response.Succeeded == true)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make("Supplier berhasil ditambah", ToastDuration.Short).Show();
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

        private async Task OnSelectWilayah()
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectListOption(_valueDisplayViewModel, SelectMultipleForConstant.Wilayah, "Wilayah"));

            if (answer != null)
            {
                Wilayah = (ValueDisplayFilterModel)answer;  
            }
        }

        private void ClearData()
        {
            Supplier = null;
            Nama = "";
            Deskripsi = "";
        }
    }
}