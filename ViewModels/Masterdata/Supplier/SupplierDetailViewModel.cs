using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Implementations.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Supplier
{
    public class SupplierDetailViewModel : BaseViewModel
    {
        private readonly SupplierService _supplierService;

        private SupplierModel? _supplier;
        public SupplierModel? Supplier
        {
            get => _supplier;
            set => SetProperty(ref _supplier, value);
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

        public SupplierDetailViewModel(SupplierService supplierService)
        {
            _supplierService = supplierService;

            GetDataCommand = new Command<string>(async (id) => await GetSupplierById(id));
            ClearDataCommand = new Command(ClearData);

            DeleteDataCommand = new Command(async () =>
            {
                try
                {
                    var response = await _supplierService.DeleteAsync(Supplier!.SupplierId!);

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

        private async Task GetSupplierById(string jenisId)
        {
            IsLoading = true;

            try
            {
                var response = await _supplierService.GetByIdAsync(jenisId);

                if (response != null && response.Succeeded == true)
                {
                    Supplier = response.Data;

                    if (!string.IsNullOrEmpty(Supplier!.Deskripsi))
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
            Supplier = null;
            HasDescription = false;
        }
    }
}