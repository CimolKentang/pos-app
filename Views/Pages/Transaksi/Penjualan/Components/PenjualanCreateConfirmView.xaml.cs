using inovasyposmobile.ViewModels.Transaksi.Penjualan;

namespace inovasyposmobile.Views.Pages.Transaksi.Penjualan.Components;

public partial class PenjualanCreateConfirmView : ContentPage
{
	public PenjualanCreateConfirmView(PenjualanCreateViewModel penjualanCreateViewModel)
	{
		InitializeComponent();
		BindingContext = penjualanCreateViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is PenjualanCreateViewModel penjualanCreateViewModel)
		{
			Console.WriteLine(penjualanCreateViewModel.Total);
		}
	}
}