using inovasyposmobile.ViewModels.Masterdata.Produk;

namespace inovasyposmobile.Views.Pages.Masterdata.Produk.Components;

public partial class ProdukCreateView : ContentPage
{
	private readonly ProdukCreateViewModel _produkCreateViewModel;
	public ProdukCreateView(ProdukCreateViewModel produkCreateViewModel)
	{
		InitializeComponent();
		BindingContext = _produkCreateViewModel = produkCreateViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_produkCreateViewModel.InitDataCommand.Execute(null);
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		// _produkCreateViewModel.ClearDataCommand.Execute(null);
	}

	private async void GoToCreateStok(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("ProdukStokCreate");
	}
}