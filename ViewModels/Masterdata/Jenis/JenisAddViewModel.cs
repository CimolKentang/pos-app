using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Implementations.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Jenis
{
    public class JenisAddViewModel : BaseViewModel
    {
        private readonly JenisService _jenisService;

        private JenisModel? _jenis;
        public JenisModel? Jenis
        {
            get => _jenis;
            set => SetProperty(ref _jenis, value);
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

        private bool _isShowBerat = false;
        public bool IsShowBerat
        {
            get => _isShowBerat;
            set => SetProperty(ref _isShowBerat, value);
        }

        private bool _isShowExpiredDate = false;
        public bool IsShowExpiredDate
        {
            get => _isShowExpiredDate;
            set => SetProperty(ref _isShowExpiredDate, value);
        }

        private bool _isShowJualOnline = false;
        public bool IsShowJualOnline
        {
            get => _isShowJualOnline;
            set => SetProperty(ref _isShowJualOnline, value);
        }

        private bool _isShowKenaPajak = false;
        public bool IsShowKenaPajak
        {
            get => _isShowKenaPajak;
            set => SetProperty(ref _isShowKenaPajak, value);
        }

        private bool _isShowLebar = false;
        public bool IsShowLebar
        {
            get => _isShowLebar;
            set => SetProperty(ref _isShowLebar, value);
        }

        private bool _isShowPanjang = false;
        public bool IsShowPanjang
        {
            get => _isShowPanjang;
            set => SetProperty(ref _isShowPanjang, value);
        }

        private bool _isShowPomTR = false;
        public bool IsShowPomTR
        {
            get => _isShowPomTR;
            set => SetProperty(ref _isShowPomTR, value);
        }

        private bool _isShowTinggi = false;
        public bool IsShowTinggi
        {
            get => _isShowTinggi;
            set => SetProperty(ref _isShowTinggi, value);
        }

        private bool _isShowUkuran = false;
        public bool IsShowUkuran
        {
            get => _isShowUkuran;
            set => SetProperty(ref _isShowUkuran, value);
        }

        private bool _isShowVarian = false;
        public bool IsShowVarian
        {
            get => _isShowVarian;
            set => SetProperty(ref _isShowVarian, value);
        }

        private bool _isShowWarna = false;
        public bool IsShowWarna
        {
            get => _isShowWarna;
            set => SetProperty(ref _isShowWarna, value);
        }

        public ICommand ClearDataCommand { get; }
        public ICommand InitDataCommand { get; }
        public ICommand AddDataCommand { get; }

        public JenisAddViewModel(JenisService jenisService)
        {
            _jenisService = jenisService;

            ClearDataCommand = new Command(ClearData);
            InitDataCommand = new Command(async () => await InitJenis());
            AddDataCommand = new Command(async () => await AddJenis());
        }

        private async Task InitJenis()
        {
            IsLoading = true;

            try
            {
                var response = await _jenisService.InitJenis();

                if (response != null && response.Succeeded == true)
                {
                    Jenis = response.Data;
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

        private async Task AddJenis()
        {
            try
            {
                Jenis!.Nama = Nama;
                Jenis.Deskripsi = Deskripsi;
                Jenis.IsShowBerat = IsShowBerat;
                Jenis.IsShowExpiredDate = IsShowExpiredDate;
                Jenis.IsShowJualOnline = IsShowJualOnline;
                Jenis.IsShowKenaPajak = IsShowKenaPajak;
                Jenis.IsShowLebar = IsShowLebar;
                Jenis.IsShowPanjang = IsShowPanjang;
                Jenis.IsShowPomTR = IsShowPomTR;
                Jenis.IsShowTinggi = IsShowTinggi;
                Jenis.IsShowUkuran = IsShowUkuran;
                Jenis.IsShowVarian = IsShowVarian;
                Jenis.IsShowWarna = IsShowWarna;

                var response = await _jenisService.CreateAsync(Jenis);

                if (response != null && response.Succeeded == true)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make("Jenis berhasil ditambah", ToastDuration.Short).Show();
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

        private void ClearData()
        {
            Jenis = null;
            Nama = "";
            Deskripsi = "";
            IsShowBerat = false;
            IsShowExpiredDate = false;
            IsShowJualOnline = false;
            IsShowKenaPajak = false;
            IsShowLebar = false;
            IsShowPanjang = false;
            IsShowPomTR = false;
            IsShowTinggi = false;
            IsShowUkuran = false;
            IsShowVarian = false;
            IsShowWarna = false;
        }
    }
}