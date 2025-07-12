using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.ViewModels.Masterdata.Produk;

namespace inovasyposmobile.Views.Pages.Masterdata.Produk;

public partial class ProdukView : ContentPage
{
	private readonly ProdukListViewModel _produkListViewModel;
	public ProdukView(ProdukListViewModel produkListViewModel)
	{
		InitializeComponent();
		BindingContext = _produkListViewModel = produkListViewModel;
	}
	
	protected override void OnAppearing()
	{
		base.OnAppearing();
		_produkListViewModel.ClearDataCommand.Execute(null);
		_produkListViewModel.GetProdukCommand.Execute(null);
	}

	private async void GoToDetail(object sender, TappedEventArgs e)
	{
		foreach (var item in JenisCollection.ItemsSource)
		{
			if (JenisCollection.ItemTemplate.CreateContent() is ViewCell cell && cell.View is VerticalStackLayout border)
			{
				border.BackgroundColor = Colors.Transparent;
			}
		}

		// Highlight selected item
		if (sender is VerticalStackLayout tappedFrame)
		{
			var selectedItem = (ProdukItemViewModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync($"ProdukDetail?ProdukId={selectedItem.Produk!.ProdukId}");
		}
    }

	private async void GoToCreate(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("JenisCreate");
	}
}