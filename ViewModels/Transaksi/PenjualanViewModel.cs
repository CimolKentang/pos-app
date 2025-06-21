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

        public ObservableCollection<PenjualanModel> Penjualans
        {
            get => _penjualans;
            set => SetProperty(ref _penjualans, value);
        }

        private string _searchText = "";
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

        private int _currentPage = 0;
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
                SearchParams.PageIndex = value;
                OnPropertyChanged(nameof(ShowCurrentPage));

                Task.Run(GetPenjualans);
            }
        }

        public int ShowCurrentPage => CurrentPage + 1;

        private int _totalPages = 0;
        public int TotalPages
        {
            get => _totalPages;
            set => SetProperty(ref _totalPages, value);
        }

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (_pageSize != value)
                {
                    SetProperty(ref _pageSize, value);
                    SearchParams.PageSize = value;
                    Task.Run(GetPenjualans);   
                }
            }
        }

        public ICommand GetPenjualansCommand { get; }
        public ICommand RefreshPenjualanCommand { get; }

        public PenjualanViewModel(IPenjualanService penjualanService, ValueDisplayViewModel valueDisplayViewModel)
        {
            _penjualanService = penjualanService;
            _valueDisplayViewModel = valueDisplayViewModel;
            GetPenjualansCommand = new Command(async () => await GetPenjualans());
            RefreshPenjualanCommand = new Command(async () => await RefreshPenjualan());
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
                await GetPenjualans();
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
                    await GetPenjualans();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task GetPenjualans()
        {
            IsLoading = true;

            try
            {
                var penjualan = await _penjualanService.GetAllAsync(SearchParams);

                if (penjualan?.Data?.Items != null)
                {
                    Penjualans.Clear();

                    TotalPages = penjualan.Data.TotalPages;

                    foreach (var penjualanItem in penjualan.Data.Items)
                    {
                        Penjualans.Add(penjualanItem);
                    }

                    Console.WriteLine(penjualan.Data.CurrentPage);
                    Console.WriteLine(penjualan.Data.PageSize);
                    Console.WriteLine(penjualan.Data.TotalItem);
                    Console.WriteLine(penjualan.Data.TotalPages);
                    Console.WriteLine(penjualan.Data.HasNextPage);
                    Console.WriteLine(penjualan.Data.HasPreviousPage);
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

        private async Task RefreshPenjualan()
        {
            IsRefreshing = true;
            await GetPenjualans();
            IsRefreshing = false;
        }
    }
}