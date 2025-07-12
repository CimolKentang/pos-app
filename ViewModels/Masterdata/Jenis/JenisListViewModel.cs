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
using inovasyposmobile.Models.Requests;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Jenis
{
    public class JenisListViewModel : BaseViewModel
    {
        private readonly IJenisService _jenisService;
        private ObservableCollection<JenisModel> _jenis = new ObservableCollection<JenisModel>();
        private SearchSortFilterPagingModel SearchParams = new SearchSortFilterPagingModel();

        private CancellationTokenSource? _debounceCts;

        public ObservableCollection<JenisModel> Jenis
        {
            get => _jenis;
            set => SetProperty(ref _jenis, value);
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

        public ICommand GetJenisCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand ClearSearchTextCommand { get; }
        public ICommand HandleScrollCommand { get; }
        public ICommand RefreshDataCommand { get; }

        public JenisListViewModel(IJenisService jenisService)
        {
            _jenisService = jenisService;

            GetJenisCommand = new Command(async () => await GetJenis());
            HandleScrollCommand = new Command(async () => await HandleScroll());
            RefreshDataCommand = new Command(async () => await RefreshData());
            ClearDataCommand = new Command(ClearData);
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
                await GetJenis();
            }
        }

        private async Task GetJenis()
        {
            IsLoading = true;
            
            try
            {
                SearchParams.PageIndex = CurrentPage;
                SearchParams.PageSize = 20;
                SearchParams.SortBy = "kode";
                SearchParams.SortDir = SortDirectionConstant.Desc;
                
                var jenis = await _jenisService.GetAllAsync(SearchParams);

                if (jenis?.Data?.Items != null)
                {
                    foreach (var jenisItem in jenis.Data.Items)
                    {
                        Jenis.Add(jenisItem);
                    }

                    HasNextPage = jenis.Data.HasNextPage;
                    TotalItem = jenis.Data.TotalItem;
                    ItemNumber = Jenis.Count;
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
                await GetJenis();

                _isHandlingScroll = false;   
            }
        }

        private async Task RefreshData()
        {
            IsRefreshing = true;
            ClearData();
            await GetJenis();
            IsRefreshing = false;
        }

        private void ClearData()
        {
            CurrentPage = 0;
            TotalItem = 0;
            ItemNumber = 0;
            HasItem = false;
            HasNextPage = false;
            Jenis.Clear();
        }
    }
}