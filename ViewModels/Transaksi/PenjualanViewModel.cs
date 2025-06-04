using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Responses;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Interfaces.Transaksi;
using inovasyposmobile.Views.Controls;

namespace inovasyposmobile.ViewModels.Transaksi
{
    public class PenjualanViewModel : BaseViewModel
    {
        private readonly IPenjualanService _penjualanService;
        private readonly ValueDisplayViewModel _valueDisplayViewModel;
        private ObservableCollection<PenjualanModel> _penjualans = new ObservableCollection<PenjualanModel>();
        private PenjualanSearchParams SearchParams = new PenjualanSearchParams();
        private CancellationTokenSource? _debounceCts;
        private string _searchText = "";

        public ObservableCollection<PenjualanModel> Penjualans
        {
            get => _penjualans;
            set => SetProperty(ref _penjualans, value);
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
                    Task.Run(OnParamsChanged);
                }
            }
        }

        public ICommand GetDatasCommand { get; }

        public PenjualanViewModel(IPenjualanService penjualanService, ValueDisplayViewModel valueDisplayViewModel)
        {
            _penjualanService = penjualanService;
            _valueDisplayViewModel = valueDisplayViewModel;
            GetDatasCommand = new Command(async () => await GetPenjualansAsync());
        }

        public ICommand ShowDialogCommand => new Command(async () =>
        {
            var answer = await Shell.Current.ShowPopupAsync(new SelectMultipleOneDialog(_valueDisplayViewModel, SelectMultipleForConstant.Gudang));
            _valueDisplayViewModel.ClearData();

            if (answer != null)
            {
                var result = (ValueDisplayFilterModel)answer;

                var filter = new ValueDisplayRequestModel
                {
                    FilterDisplay = result.Display,
                    FilterId = result.Value
                };

                SearchParams.FilterGudang.Add(filter);
                await GetPenjualansAsync();
            }
            else
            {
            }
        });

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
                    await GetPenjualansAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task GetPenjualansAsync()
        {
            IsLoading = true;

            try
            {
                var penjualan = await _penjualanService.GetAllAsync(SearchParams);

                if (penjualan?.Data?.Items != null)
                {
                    Penjualans.Clear();

                    foreach (var penjualanItem in penjualan.Data.Items)
                    {
                        Penjualans.Add(penjualanItem);
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