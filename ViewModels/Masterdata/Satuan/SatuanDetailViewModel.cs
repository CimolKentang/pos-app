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
    public class SatuanDetailViewModel : BaseViewModel
    {
        private readonly SatuanService _satuanService;

        private SatuanModel? _satuan;
        public SatuanModel? Satuan
        {
            get => _satuan;
            set => SetProperty(ref _satuan, value);
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

        public SatuanDetailViewModel(SatuanService satuanService)
        {
            _satuanService = satuanService;

            GetDataCommand = new Command<string>(async (id) => await GetSatuanById(id));
            ClearDataCommand = new Command(ClearData);

            DeleteDataCommand = new Command(async () =>
            {
                try
                {
                    var response = await _satuanService.DeleteAsync(Satuan!.SatuanId!);

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

        private async Task GetSatuanById(string jenisId)
        {
            IsLoading = true;

            try
            {
                var response = await _satuanService.GetByIdAsync(jenisId);

                if (response != null && response.Succeeded == true)
                {
                    Satuan = response.Data;

                    if (!string.IsNullOrEmpty(Satuan!.Deskripsi))
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
            Satuan = null;
            HasDescription = false;
        }
    }
}