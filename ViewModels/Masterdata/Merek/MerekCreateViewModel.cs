using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Implementations.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Merek
{
    public class MerekCreateViewModel : BaseViewModel
    {
        private readonly MerekService _merekService;

        private MerekModel? _merek;
        public MerekModel? Merek
        {
            get => _merek;
            set => SetProperty(ref _merek, value);
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

        public ICommand ClearDataCommand { get; }
        public ICommand InitDataCommand { get; }
        public ICommand AddDataCommand { get; }

        public MerekCreateViewModel(MerekService merekService)
        {
            _merekService = merekService;

            InitDataCommand = new Command(async () => await InitMerek());
            AddDataCommand = new Command(async () => await AddMerek());
            ClearDataCommand = new Command(ClearData);
        }

        private async Task InitMerek()
        {
            IsLoading = true;

            try
            {
                var response = await _merekService.InitMerek();

                if (response != null && response.Succeeded == true)
                {
                    Merek = response.Data;
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

        private async Task AddMerek()
        {
            try
            {
                Merek!.Nama = Nama;
                Merek.Deskripsi = Deskripsi;

                var response = await _merekService.CreateAsync(Merek);

                if (response != null && response.Succeeded == true)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make("Merek berhasil ditambah", ToastDuration.Short).Show();
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
            Merek = null;
            Nama = "";
            Deskripsi = "";
        }
    }
}