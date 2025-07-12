using inovasyposmobile.Services.Interfaces.Auth;
using inovasyposmobile.Views.Pages.Masterdata.Jenis;
using inovasyposmobile.Views.Pages.Masterdata.Jenis.Components;
using inovasyposmobile.Views.Pages.Masterdata.Merek;
using inovasyposmobile.Views.Pages.Masterdata.Merek.Components;
using inovasyposmobile.Views.Pages.Masterdata.Pelanggan;
using inovasyposmobile.Views.Pages.Masterdata.Produk;
using inovasyposmobile.Views.Pages.Masterdata.Produk.Components;
using inovasyposmobile.Views.Pages.Masterdata.Satuan;
using inovasyposmobile.Views.Pages.Masterdata.Satuan.Components;
using inovasyposmobile.Views.Pages.Masterdata.Supplier;
using inovasyposmobile.Views.Pages.Transaksi.Penjualan;
using inovasyposmobile.Views.Pages.Transaksi.Penjualan.Components;

namespace inovasyposmobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("Penjualan", typeof(PenjualanView));
		Routing.RegisterRoute("PenjualanDetail", typeof(PenjualanDetailView));
		Routing.RegisterRoute("PenjualanCreate", typeof(PenjualanCreateView));
		Routing.RegisterRoute("PenjualanCreateConfirm", typeof(PenjualanCreateConfirmView));

		Routing.RegisterRoute("Produk", typeof(ProdukView));
		Routing.RegisterRoute("ProdukDetail", typeof(ProdukDetailView));

		Routing.RegisterRoute("Merek", typeof(MerekView));
		Routing.RegisterRoute("MerekDetail", typeof(MerekDetailView));
		Routing.RegisterRoute("MerekCreate", typeof(MerekCreateView));

		Routing.RegisterRoute("Jenis", typeof(JenisView));
		Routing.RegisterRoute("JenisDetail", typeof(JenisDetailView));
		Routing.RegisterRoute("JenisCreate", typeof(JenisCreateView));

		Routing.RegisterRoute("Satuan", typeof(SatuanView));
		Routing.RegisterRoute("SatuanDetail", typeof(SatuanDetailView));
		Routing.RegisterRoute("SatuanCreate", typeof(SatuanCreateView));

		Routing.RegisterRoute("Supplier", typeof(SupplierView));

		Routing.RegisterRoute("Pelanggan", typeof(PelangganView));
	}
}
