using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Implementations.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Satuan
{
    public class SatuanCreateViewModel : BaseViewModel
    {
        private readonly SatuanService _satuanService;

        private SatuanModel? _satuan;
        public SatuanModel? Satuan
        {
            get => _satuan;
            set => SetProperty(ref _satuan, value);
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

        public SatuanCreateViewModel(SatuanService satuanService)
        {
            _satuanService = satuanService;

            InitDataCommand = new Command(async () => await InitSatuan());
            AddDataCommand = new Command(async () => await AddSatuan());
            ClearDataCommand = new Command(ClearData);
        }

        private async Task InitSatuan()
        {
            IsLoading = true;

            try
            {
                var response = await _satuanService.InitSatuan();

                if (response != null && response.Succeeded == true)
                {
                    Satuan = response.Data;
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

        private async Task AddSatuan()
        {
            try
            {
                Satuan!.Nama = Nama;
                Satuan.Deskripsi = Deskripsi;

                var response = await _satuanService.CreateAsync(Satuan);

                if (response != null && response.Succeeded == true)
                {
                    await Shell.Current.GoToAsync("..");
                    await Toast.Make("Satuan berhasil ditambah", ToastDuration.Short).Show();
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
            Satuan = null;
            Nama = "";
            Deskripsi = "";
        }
    }
}