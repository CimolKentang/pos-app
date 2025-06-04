using inovasyposmobile.ViewModels.Transaksi.Penjualan;

namespace inovasyposmobile.Views.Pages.Transaksi.Penjualan.Components;

public partial class PenjualanCreateView : ContentPage
{
	public PenjualanCreateView(PenjualanCreateViewModel penjualanCreateViewModel)
	{
		InitializeComponent();
		BindingContext = penjualanCreateViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is PenjualanCreateViewModel penjualanCreateViewModel)
		{
			if (penjualanCreateViewModel.Produks.Count < 1)
			{
				penjualanCreateViewModel.InitPenjualanKasirCommand.Execute(null);
			}
		}
	}

	private void ProcessPenjualan(object sender, EventArgs e)
	{
		if (BindingContext is PenjualanCreateViewModel penjualanCreateViewModel)
		{
			penjualanCreateViewModel.ProcessPenjualanKasirCommand.Execute(null);
			Shell.Current.GoToAsync("PenjualanCreateConfirm");	
		}
	}
}