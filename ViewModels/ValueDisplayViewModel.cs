using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Service.Autofill;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Constants;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Services.Interfaces;

namespace inovasyposmobile.ViewModels
{
    public class ValueDisplayViewModel : BaseViewModel
    {
        private readonly IValueDisplayService _valueDisplayService;

        private ObservableCollection<ValueDisplayFilterModel> _datas = new ObservableCollection<ValueDisplayFilterModel>();
        public ObservableCollection<ValueDisplayFilterModel> Datas
        {
            get => _datas;
            set => SetProperty(ref _datas, value);
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
                }
            }
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

        private bool _hasNextPage = true;
        public bool HasNextPage
        {
            get => _hasNextPage;
            set => SetProperty(ref _hasNextPage, value);
        }

        private int _currentPage = 0;
        public int CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        private bool _hasData = false;
        public bool HasData
        {
            get => _hasData;
            set
            {
                if (_hasData != value)
                {
                    SetProperty(ref _hasData, value);
                    OnPropertyChanged(nameof(HasNoData));
                }
            }
        }

        public bool HasNoData => !HasData;

        private bool _isHandlingScroll = false;

        public ICommand GetDatasCommand { get; }
        public ICommand ScrollDataCommand { get; }

        public ValueDisplayViewModel(IValueDisplayService valueDisplayService)
        {
            _valueDisplayService = valueDisplayService;
            GetDatasCommand = new Command<string>(async (selectMultipleForConstant) => await GetDatasAsync(selectMultipleForConstant));
            ScrollDataCommand = new Command<string>(async (selectMultipleForConstant) => await ScrollData(selectMultipleForConstant));
        }

        private async Task GetDatasAsync(string selectMultipleFor)
        {
            IsLoading = true;

            try
            {
                var response = await _valueDisplayService.GetAllAsync(selectMultipleFor, SearchText, CurrentPage);

                if (response?.Data?.Items != null)
                {
                    Console.WriteLine("POPUP");
                    Console.WriteLine(response.Data.TotalItem);
                    Console.WriteLine(response.Data);

                    TotalItem = response.Data.TotalItem;
                    HasNextPage = response.Data.HasNextPage;

                    foreach (var penjualanItem in response.Data.Items)
                    {
                        Datas.Add(penjualanItem);
                    }

                    ItemNumber = Datas.Count;
                    if (ItemNumber > 0)
                    {
                        HasData = true;
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

        private async Task ScrollData(string selectMultipleFor)
        {
            if (_isHandlingScroll) return;

            _isHandlingScroll = true;

            if (HasNextPage == true)
            {
                CurrentPage += 1;
                await GetDatasAsync(selectMultipleFor);
            }

            _isHandlingScroll = false;
        }

        public void ClearData()
        {
            Datas.Clear();
            CurrentPage = 0;
            ItemNumber = 0;
            TotalItem = 0;
            SearchText = "";
        }
    }
}