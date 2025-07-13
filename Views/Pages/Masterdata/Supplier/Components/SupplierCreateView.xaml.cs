using inovasyposmobile.ViewModels.Masterdata.Supplier;

namespace inovasyposmobile.Views.Pages.Masterdata.Supplier.Components;

public partial class SupplierCreateView : ContentPage
{
	private readonly SupplierCreateViewModel _supplierCreateViewModel;
	public SupplierCreateView(SupplierCreateViewModel supplierCreateViewModel)
	{
		InitializeComponent();
		BindingContext = _supplierCreateViewModel = supplierCreateViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_supplierCreateViewModel.InitDataCommand.Execute(null);
	}

	protected override void OnDisappearing()
	{
		base.OnDisappearing();
		_supplierCreateViewModel.ClearDataCommand.Execute(null);
	}
}