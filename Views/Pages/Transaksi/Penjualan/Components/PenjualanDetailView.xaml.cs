using inovasyposmobile.ViewModels.Transaksi.Penjualan;

namespace inovasyposmobile.Views.Pages.Transaksi.Penjualan.Components;

[QueryProperty(nameof(PenjualanId), "PenjualanId")]
public partial class PenjualanDetailView : ContentPage
{
	private readonly PenjualanDetailViewModel _viewModel;
	public PenjualanDetailView(PenjualanDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

	public string PenjualanId
	{
		set
		{
			if (value != null)
			{
				_viewModel.GetDataCommand.Execute(value);
			}
			else
			{
				Shell.Current.GoToAsync("..");
			}
		}
	}
}