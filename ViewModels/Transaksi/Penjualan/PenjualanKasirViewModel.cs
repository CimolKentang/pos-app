using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using inovasyposmobile.Exceptions;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Interfaces.Transaksi;

namespace inovasyposmobile.ViewModels.Transaksi.Penjualan
{
    public class PenjualanKasirViewModel : BaseViewModel
    {
        private readonly IPenjualanService _penjualanService;
        private PenjualanModel? _penjualan;

        public PenjualanModel? Penjualan
        {
            get => _penjualan;
            set => SetProperty(ref _penjualan, value);
        }

        public PenjualanKasirViewModel(IPenjualanService penjualanService)
        {
            _penjualanService = penjualanService;
        }

        public async Task InitPenjualanKasir()
        {
            IsLoading = true;

            try
            {
                var penjualan = await _penjualanService.InitPenjualanKasir();

                if (penjualan != null && penjualan.Succeeded == true)
                {
                    Penjualan = penjualan.Data;
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
    }
}