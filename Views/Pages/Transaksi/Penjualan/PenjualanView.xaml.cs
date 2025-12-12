using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using inovasyposmobile.Models.Transaksi.Penjualan;
using inovasyposmobile.Services.Implementations.Auth;
using inovasyposmobile.ViewModels.Transaksi.Penjualan;
using inovasyposmobile.Views.Controls;

namespace inovasyposmobile.Views.Pages.Transaksi.Penjualan;

public partial class PenjualanView : ContentPage
{
	private readonly AuthService _authService;
	private readonly PenjualanListViewModel _penjualanViewModel;
	public PenjualanView(PenjualanListViewModel penjualanListViewModel, AuthService authService)
	{
		InitializeComponent();
		BindingContext = _penjualanViewModel = penjualanListViewModel;
		_authService = authService;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_penjualanViewModel.ClearDataCommand.Execute(null);
		_penjualanViewModel.GetPenjualansCommand.Execute(null);
	}

	private void GoToPenjualanDetail(object sender, EventArgs e)
	{
		// Reset all items' background color first
		foreach (var item in PenjualanCollection.ItemsSource)
		{
			if (PenjualanCollection.ItemTemplate.CreateContent() is ViewCell cell && cell.View is Grid border)
			{
				border.BackgroundColor = Colors.Transparent;
			}
		}

		// Highlight selected item
		if (sender is Grid tappedFrame)
		{
			var selectedItem = (PenjualanModel)tappedFrame.BindingContext;
			Shell.Current.GoToAsync($"PenjualanDetail?PenjualanId={selectedItem.PenjualanId}");
		}
	}

	private void Logout(object sender, EventArgs e)
	{
		_authService.Logout();
	}
}