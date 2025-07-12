using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Produk
{
    public class ProdukDetailViewModel : BaseViewModel
    {
        private readonly IProdukService _produkService;

        private ProdukModel? _produk;
        public ProdukModel? Produk
        {
            get => _produk;
            set => SetProperty(ref _produk, value);
        }

        private bool _hasDescription = false;
        public bool HasDescription
        {
            get => _hasDescription;
            set => SetProperty(ref _hasDescription, value);
        }

        private bool _hasImage = false;
        public bool HasImage
        {
            get => _hasImage;
            set => SetProperty(ref _hasImage, value);
        }

        private string _kode = "";
        public string Kode
        {
            get => _kode;
            set => SetProperty(ref _kode, value);
        }
        
        private string _satuan = "";
        public string Satuan
        {
            get => _satuan;
            set => SetProperty(ref _satuan, value);
        }

        private string _jenis = "";
        public string Jenis
        {
            get => _jenis;
            set => SetProperty(ref _jenis, value);
        }

        private string _merek = "";
        public string Merek
        {
            get => _merek;
            set => SetProperty(ref _merek, value);
        }

        private string _supplier = "";
        public string Supplier
        {
            get => _supplier;
            set => SetProperty(ref _supplier, value);
        }

        public ICommand GetDataCommand { get; }
        public ICommand ClearDataCommand { get; }
        public ICommand DeleteDataCommand { get; }

        public ProdukDetailViewModel(IProdukService produkService)
        {
            _produkService = produkService;

            GetDataCommand = new Command<string>(async (id) => await GetProdukById(id));
            ClearDataCommand = new Command(ClearData);

            DeleteDataCommand = new Command(async () =>
            {
                try
                {
                    var response = await _produkService.DeleteAsync(Produk!.ProdukId!);

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

        private async Task GetProdukById(string jenisId)
        {
            IsLoading = true;

            try
            {
                var response = await _produkService.GetByIdAsync(jenisId);

                if (response != null && response.Succeeded == true)
                {
                    var produkItem = response.Data;
                    Produk = response.Data;

                    if (!string.IsNullOrEmpty(Produk!.Deskripsi))
                    {
                        HasDescription = true;
                    }
                    else
                    {
                        HasDescription = false;
                    }

                    if (!string.IsNullOrEmpty(Produk.Gambar))
                    {
                        HasImage = true;
                    }
                    else
                    {
                        HasImage = false;
                    }

                    Kode = Produk.Kode!;
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
            Produk = null;
            HasImage = false;
            HasDescription = false;
        }
    }
}