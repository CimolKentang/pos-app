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
        private string _searchText = "";

        public ObservableCollection<ValueDisplayFilterModel> Datas
        {
            get => _datas;
            set => SetProperty(ref _datas, value);
        }

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

        public ICommand GetDatasCommand { get; }

        public ValueDisplayViewModel(IValueDisplayService valueDisplayService)
        {
            _valueDisplayService = valueDisplayService;
            GetDatasCommand = new Command<string>(async (selectMultipleForConstant) => await GetDatasAsync(selectMultipleForConstant));
        }

        private async Task GetDatasAsync(string selectMultipleFor)
        {
            IsLoading = true;

            try
            {
                var response = await _valueDisplayService.GetAllAsync(selectMultipleFor, SearchText);

                if (response?.Data?.Items != null)
                {
                    foreach (var penjualanItem in response.Data.Items)
                    {
                        Datas.Add(penjualanItem);
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

        public void ClearData()
        {
            Datas.Clear();
        }
    }
}