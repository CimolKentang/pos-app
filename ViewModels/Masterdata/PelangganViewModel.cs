using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata
{
    public class PelangganViewModel : BaseViewModel
    {
        private readonly IPelangganService _pelangganService;
        private ObservableCollection<PelangganModel> _pelanggan = new ObservableCollection<PelangganModel>();
        private ObservableCollection<ValueDisplayFilterModel> _pelanggansValueDisplayFilter = new ObservableCollection<ValueDisplayFilterModel>();
        private PelangganSearchParams SearchParams = new PelangganSearchParams();
        private CancellationTokenSource? _debounceCts;
        private string _searchText = "";
        public ObservableCollection<PelangganModel> Pelanggans
        {
            get => _pelanggan;
            set => SetProperty(ref _pelanggan, value);
        }

        public ObservableCollection<ValueDisplayFilterModel> PelanggansValueDisplayFilter
        {
            get => _pelanggansValueDisplayFilter;
            set => SetProperty(ref _pelanggansValueDisplayFilter, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    SetProperty(ref _searchText, value);
                    SearchParams.Search = value;
                    // Task.Run(OnParamsChanged);
                }
            }
        }

        public ICommand GetDatasCommand { get; }

        public PelangganViewModel(IPelangganService pelangganService)
        {
            _pelangganService = pelangganService;
            GetDatasCommand = new Command(async () => await GetPelanggansAsync());
        }

        private async Task OnParamsChanged()
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();
            var token = _debounceCts.Token;

            try
            {
                await Task.Delay(700, token);

                if (!token.IsCancellationRequested)
                {
                    await GetPelanggansAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task GetPelanggansAsync()
        {
            IsLoading = true;

            try
            {
                PelangganSearchParams searchParams = new PelangganSearchParams();
                searchParams.IsValueDisPlay = true;

                var penjualan = await _pelangganService.GetAllValueDisplayAsync(searchParams);

                if (penjualan?.Data?.Items != null)
                {
                    foreach (var penjualanItem in penjualan.Data.Items)
                    {
                        PelanggansValueDisplayFilter.Add(penjualanItem);
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
    }
}