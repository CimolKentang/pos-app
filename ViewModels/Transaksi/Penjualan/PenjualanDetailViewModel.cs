using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Interfaces.Transaksi;

namespace inovasyposmobile.ViewModels.Transaksi.Penjualan
{
    public class PenjualanDetailViewModel : BaseViewModel
    {
        private readonly IPenjualanService _penjualanService;
        private PenjualanModel? _penjualan;
        private bool _isKonsinyasi;
        private bool _hasSalesChannelReferensi;
        private bool _hasDescription;
        public ICommand GetDataCommand { get; }
        public PenjualanDetailViewModel(IPenjualanService penjualanService)
        {
            _penjualanService = penjualanService;
            GetDataCommand = new Command<string>(async (id) => await GetPenjualanById(id));
        }
        
        public PenjualanModel? Penjualan
        {
            get => _penjualan;
            set => SetProperty(ref _penjualan, value);
        }

        public bool IsKonsinyasi
        {
            get => _isKonsinyasi;
            set => SetProperty(ref _isKonsinyasi, value);
        }

        public bool HasSalesChannelReferensi
        {
            get => _hasSalesChannelReferensi;
            set
            {
                SetProperty(ref _hasSalesChannelReferensi, value);
                OnPropertyChanged(nameof(HasNotSalesChannelReferensi));
            }
        }

        public bool HasNotSalesChannelReferensi => !HasSalesChannelReferensi;

        public bool HasDescription
        {
            get => _hasDescription;
            set
            {
                SetProperty(ref _hasDescription, value);
                OnPropertyChanged(nameof(HasNotDescription));
            }
        }

        public bool HasNotDescription => !HasDescription;

        private async Task GetPenjualanById(string penjualanId)
        {
            IsLoading = true;

            try
            {
                var response = await _penjualanService.GetByIdAsync(penjualanId);

                if (response != null && response.Succeeded == true)
                {
                    Penjualan = response.Data;

                    if (!string.IsNullOrEmpty(Penjualan!.KonsinyasiId))
                    {
                        IsKonsinyasi = true;
                    }
                    else
                    {
                        IsKonsinyasi = false;
                    }

                    if (!string.IsNullOrEmpty(Penjualan.SalesChannelId))
                    {
                        HasSalesChannelReferensi = true;
                    }
                    else
                    {
                        HasSalesChannelReferensi = false;
                    }

                    if (!string.IsNullOrEmpty(Penjualan.Deskripsi))
                    {
                        HasDescription = true;
                    }
                    else
                    {
                        HasDescription = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}