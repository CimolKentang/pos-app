using inovasyposmobile.Views.Pages.Transaksi.Penjualan;
using inovasyposmobile.Views.Pages.Transaksi.Penjualan.Components;

namespace inovasyposmobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Routing.RegisterRoute("Penjualan", typeof(PenjualanView));
		Routing.RegisterRoute("PenjualanDetail", typeof(PenjualanDetailView));
		Routing.RegisterRoute("PenjualanCreateConfirm", typeof(PenjualanCreateConfirmView));
	}
}
