using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Implementations.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Pelanggan
{
    public class PelangganListViewModel : BaseViewModel
    {
        private readonly PelangganService _pelangganService;
        private ObservableCollection<PelangganModel> _pelanggan = new ObservableCollection<PelangganModel>();
        private PelangganSearchParams SearchParams = new PelangganSearchParams();

        private CancellationTokenSource? _debounceCts;

        public ObservableCollection<PelangganModel> Pelanggan
        {
            get => _pelanggan;
            set => SetProperty(ref _pelanggan, value);
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

                    if (!string.IsNullOrEmpty(value))
                    {
                        SearchTextNotEmpty = true;
                    }
                    else
                    {
                        SearchTextNotEmpty = false;
                    }
                    
                    Task.Run(OnSearchTextChanged);
                }
            }
        }

        private bool _searchTextNotEmpty = false;
        public bool SearchTextNotEmpty
        {
            get => _searchTextNotEmpty;
            set => SetProperty(ref _searchTextNotEmpty, value);
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

        public ICommand GetPelangganCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand ClearSearchTextCommand { get; }
        public ICommand HandleScrollCommand { get; }
        public ICommand RefreshDataCommand { get; }

        public PelangganListViewModel(PelangganService pelangganService)
        {
            _pelangganService = pelangganService;

            GetPelangganCommand = new Command(async () => await GetPelanggan());
            ClearDataCommand = new Command(ClearData);
            HandleScrollCommand = new Command(async () => await HandleScroll());
            RefreshDataCommand = new Command(async () => await RefreshData());
            ClearSearchTextCommand = new Command(() => SearchText = "");
        }

        private async Task OnSearchTextChanged()
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();
            var token = _debounceCts.Token;

            await Task.Delay(700, token);

            if (!token.IsCancellationRequested)
            {
                ClearData();
                await GetPelanggan();
            }
        }

        private async Task GetPelanggan()
        {
            IsLoading = true;

            try
            {
                SearchParams.PageIndex = CurrentPage;
                SearchParams.PageSize = 20;
                SearchParams.SortBy = "kode";
                SearchParams.SortDir = SortDirectionConstant.Desc;
                SearchParams.IsValueDisPlay = false;

                var pelanggan = await _pelangganService.GetAllAsync(SearchParams);

                if (pelanggan?.Data?.Items != null)
                {
                    foreach (var pelangganItem in pelanggan.Data.Items)
                    {
                        Pelanggan.Add(pelangganItem);
                    }

                    HasNextPage = pelanggan.Data.HasNextPage;
                    TotalItem = pelanggan.Data.TotalItem;
                    ItemNumber = Pelanggan.Count;
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
                await GetPelanggan();

                _isHandlingScroll = false;   
            }
        }

        private async Task RefreshData()
        {
            IsRefreshing = true;
            ClearData();
            await GetPelanggan();
            IsRefreshing = false;
        }

        private void ClearData()
        {
            CurrentPage = 0;
            TotalItem = 0;
            ItemNumber = 0;
            HasItem = false;
            Pelanggan.Clear();
        }
    }
}