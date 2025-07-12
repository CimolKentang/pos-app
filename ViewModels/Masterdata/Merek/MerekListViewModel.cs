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
using inovasyposmobile.Services.Implementations.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Merek
{
    public class MerekListViewModel : BaseViewModel
    {
        private readonly MerekService _merekService;
        private ObservableCollection<MerekModel> _merek = new ObservableCollection<MerekModel>();
        private SearchSortFilterPagingModel SearchParams = new SearchSortFilterPagingModel();

        private CancellationTokenSource? _debounceCts;

        public ObservableCollection<MerekModel> Merek
        {
            get => _merek;
            set => SetProperty(ref _merek, value);
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

        public ICommand GetDataCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand ClearSearchTextCommand { get; }
        public ICommand HandleScrollCommand { get; }
        public ICommand RefreshDataCommand { get; }

        public MerekListViewModel(MerekService jenisService)
        {
            _merekService = jenisService;

            GetDataCommand = new Command(async () => await GetData());
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
                await GetData();
            }
        }

        private async Task GetData()
        {
            IsLoading = true;
            
            try
            {
                SearchParams.PageIndex = CurrentPage;
                SearchParams.PageSize = 20;
                SearchParams.SortBy = "kode";
                SearchParams.SortDir = SortDirectionConstant.Desc;
                
                var merek = await _merekService.GetAllAsync(SearchParams);

                if (merek?.Data?.Items != null)
                {
                    foreach (var jenisItem in merek.Data.Items)
                    {
                        Merek.Add(jenisItem);
                    }

                    HasNextPage = merek.Data.HasNextPage;
                    TotalItem = merek.Data.TotalItem;
                    ItemNumber = Merek.Count;
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
                await GetData();

                _isHandlingScroll = false;   
            }
        }

        private async Task RefreshData()
        {
            IsRefreshing = true;
            ClearData();
            await GetData();
            IsRefreshing = false;
        }

        private void ClearData()
        {
            CurrentPage = 0;
            TotalItem = 0;
            ItemNumber = 0;
            HasItem = false;
            HasNextPage = false;
            Merek.Clear();
        }
    }
}