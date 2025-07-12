using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Jenis
{
    public class JenisDetailViewModel : BaseViewModel
    {
        private readonly IJenisService _jenisService;

        private JenisModel? _jenis;
        public JenisModel? Jenis
        {
            get => _jenis;
            set => SetProperty(ref _jenis, value);
        }

        private bool _hasDescription = false;
        public bool HasDescription
        {
            get => _hasDescription;
            set => SetProperty(ref _hasDescription, value);
        }

        public ICommand GetDataCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand DeleteDataCommand { get; }

        public JenisDetailViewModel(IJenisService jenisService)
        {
            _jenisService = jenisService;

            GetDataCommand = new Command<string>(async (id) => await GetJenisById(id));
            ClearDataCommand = new Command(ClearData);

            DeleteDataCommand = new Command(async () =>
            {
                try
                {
                    var response = await _jenisService.DeleteAsync(Jenis!.JenisId!);

                    if (response != null && response.Succeeded == true)
                    {
                        await Shell.Current.GoToAsync("..");
                        ClearData();
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
            });
        }

        private async Task GetJenisById(string jenisId)
        {
            IsLoading = true;

            try
            {
                var response = await _jenisService.GetByIdAsync(jenisId);

                if (response != null && response.Succeeded == true)
                {
                    Jenis = response.Data;

                    if (!string.IsNullOrEmpty(Jenis!.Deskripsi))
                    {
                        HasDescription = true;
                    }
                    else
                    {
                        HasDescription = false;
                    }
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

        private void ClearData()
        {
            Jenis = null;
            HasDescription = false;
        }
    }
}