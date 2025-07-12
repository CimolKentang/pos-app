using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inovasyposmobile.ViewModels.Home
{
    public class HomeViewModel
    {
        public List<PageListModel> Pages { get; set; } = new List<PageListModel>
        { 
            new PageListModel {Title = "Penjualan", Page = "PenjualanCreate", Icon = "\ue547"},
            new PageListModel {Title = "Jenis", Page = "Jenis", Icon = "\ue1bd"},
            new PageListModel {Title = "Supplier", Page = "Supplier", Icon = "\ue7fd"},
            new PageListModel {Title = "Produk", Page = "Produk", Icon = "\uf1cc"},
            new PageListModel {Title = "Merek", Page = "Merek", Icon = "\uea12"},
            new PageListModel {Title = "Satuan", Page = "Satuan", Icon = "\ueb5f"},
            new PageListModel {Title = "Pelanggan", Page = "Pelanggan", Icon = "\ue7fd"},
        };
    }

    public class PageListModel
    {
        public string Title { get; set; } = "";
        public string Page { get; set; } = "";
        public string Icon { get; set; } = "";
    }
}