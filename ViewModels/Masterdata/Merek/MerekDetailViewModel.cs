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

namespace inovasyposmobile.ViewModels.Masterdata.Merek
{
    public class MerekDetailViewModel : BaseViewModel
    {
        private readonly MerekService _merekService;

        private MerekModel? _merek;
        public MerekModel? Merek
        {
            get => _merek;
            set => SetProperty(ref _merek, value);
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

        public MerekDetailViewModel(MerekService merekService)
        {
            _merekService = merekService;

            GetDataCommand = new Command<string>(async (id) => await GetMerekById(id));
            ClearDataCommand = new Command(ClearData);

            DeleteDataCommand = new Command(async () =>
            {
                try
                {
                    var response = await _merekService.DeleteAsync(Merek!.MerekId!);

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

        private async Task GetMerekById(string jenisId)
        {
            IsLoading = true;

            try
            {
                var response = await _merekService.GetByIdAsync(jenisId);

                if (response != null && response.Succeeded == true)
                {
                    Merek = response.Data;

                    if (!string.IsNullOrEmpty(Merek!.Deskripsi))
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
            Merek = null;
            HasDescription = false;
        }
    }
}