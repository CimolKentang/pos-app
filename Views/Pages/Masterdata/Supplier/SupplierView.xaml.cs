using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.ViewModels.Masterdata.Supplier;

namespace inovasyposmobile.Views.Pages.Masterdata.Supplier;

public partial class SupplierView : ContentPage
{
	private readonly SupplierListViewModel _supplierListViewModel;
	public SupplierView(SupplierListViewModel supplierListViewModel)
	{
		InitializeComponent();
		BindingContext = _supplierListViewModel = supplierListViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_supplierListViewModel.ClearDataCommand.Execute(null);
		_supplierListViewModel.GetDataCommand.Execute(null);
	}

	private async void GoToDetail(object sender, TappedEventArgs e)
	{
		foreach (var item in SupplierCollection.ItemsSource)
		{
			if (SupplierCollection.ItemTemplate.CreateContent() is ViewCell cell && cell.View is VerticalStackLayout border)
			{
				border.BackgroundColor = Colors.Transparent;
			}
		}

		// Highlight selected item
		if (sender is VerticalStackLayout tappedFrame)
		{
			var selectedItem = (SupplierModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync($"SupplierDetail?SupplierId={selectedItem.SupplierId}");
		}
    }

	private async void GoToCreate(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("SupplierCreate");
    }
}