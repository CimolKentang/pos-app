using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.Services.Interfaces.Masterdata;

namespace inovasyposmobile.ViewModels.Masterdata.Produk
{
    public class ProdukWithStokListViewModel : BaseViewModel
    {
        private readonly IProdukService _produkService;
        private ObservableCollection<ProdukWithStokViewModel> _produkWithStoks = new();
        private ProdukSearchParams _searchParams;
        public string Title { get; set; } = "Produk With Stok List";

        public ObservableCollection<ProdukWithStokViewModel> ProdukWithStoks
        {
            get => _produkWithStoks;
            set => SetProperty(ref _produkWithStoks, value);
        }

        public ProdukWithStokListViewModel(IProdukService produkService)
        {
            _produkService = produkService;
            _searchParams = new ProdukSearchParams();
        }

        public async Task GetProdukWithStok(string gudangId, string namaGudang)
        {
            IsLoading = true;

            try
            {
                var filterGudang = new ValueDisplayRequestModel
                {
                    FilterId = gudangId,
                    FilterDisplay = namaGudang
                };

                _searchParams.FilterGudang.Add(filterGudang);

                var produkWithStok = await _produkService.GetProdukWithStoks(_searchParams);

                if (produkWithStok?.Data?.Items != null)
                {
                    foreach (var produkItem in produkWithStok.Data.Items)
                    {
                        Console.WriteLine(produkItem.NamaProduk);
                        var produkWithStokVM = new ProdukWithStokViewModel();
                        produkWithStokVM.ProdukWithStok = produkItem;

                        ProdukWithStoks.Add(produkWithStokVM);
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

        public void EmptyProdukWithStoks()
        {
            ProdukWithStoks.Clear();
        }
    }
}