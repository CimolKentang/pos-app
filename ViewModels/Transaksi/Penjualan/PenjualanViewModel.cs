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

namespace inovasyposmobile.ViewModels.Transaksi.Penjualan
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
                    Task.Run(OnSearchTextChanged);
                }
            }
        }

        private bool _hasNextPage = false;
        public bool HasNextPage
        {
            get => _hasNextPage;
            set
            {
                SetProperty(ref _hasNextPage, value);
                OnPropertyChanged(nameof(HasNoNextPage));
            }
        }

        public bool HasNoNextPage => !HasNextPage;

        private int _currentPage = 0;
        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private int _totalItem = 0;
        public int TotalItem
        {
            get => _totalItem;
            set => SetProperty(ref _totalItem, value);
        }

        private int _itemNumber = 0;
        public int ItemNumber
        {
            get => _itemNumber;
            set => SetProperty(ref _itemNumber, value);
        }

        private bool _hasItem = false;
        public bool HasItem
        {
            get => _hasItem;
            set => SetProperty(ref _hasItem, value);
        }

        private bool _isHandlingScroll = false;

        public ICommand GetPenjualansCommand { get; }
        public ICommand RefreshPenjualanCommand { get; }
        public ICommand HandleScrollCommand { get; }

        public PenjualanViewModel(IPenjualanService penjualanService, ValueDisplayViewModel valueDisplayViewModel)
        {
            _penjualanService = penjualanService;
            _valueDisplayViewModel = valueDisplayViewModel;
            GetPenjualansCommand = new Command(async () => await GetPenjualans());
            RefreshPenjualanCommand = new Command(async () => await RefreshPenjualan());
            HandleScrollCommand = new Command(async () => await HandleScroll());
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

        private async Task OnSearchTextChanged()
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();
            var token = _debounceCts.Token;

            try
            {
                await Task.Delay(700, token);

                if (!token.IsCancellationRequested)
                {
                    ClearData();
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
                SearchParams.PageIndex = CurrentPage;
                var penjualan = await _penjualanService.GetAllAsync(SearchParams);

                if (penjualan?.Data?.Items != null)
                {
                    foreach (var penjualanItem in penjualan.Data.Items)
                    {
                        Penjualans.Add(penjualanItem);
                    }

                    HasNextPage = penjualan.Data.HasNextPage;
                    TotalItem = penjualan.Data.TotalItem;
                    ItemNumber = Penjualans.Count;
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
                if (TotalItem > 0)
                {
                    HasItem = true;
                }
            }
        }

        private async Task HandleScroll()
        {
            if (_isHandlingScroll == true) return;

            if (HasNextPage)
            {
                _isHandlingScroll = true;

                CurrentPage += 1;
                await GetPenjualans();

                _isHandlingScroll = false;   
            }
        }

        private async Task RefreshPenjualan()
        {
            IsRefreshing = true;
            ClearData();
            await GetPenjualans();
            IsRefreshing = false;
        }

        private void ClearData()
        {
            CurrentPage = 0;
            TotalItem = 0;
            ItemNumber = 0;
            HasItem = false;
            Penjualans.Clear();
        }
    }
}