using inovasyposmobile.ViewModels.Masterdata.Supplier;

namespace inovasyposmobile.Views.Pages.Masterdata.Supplier.Components;

[QueryProperty(nameof(SupplierId), "SupplierId")]
public partial class SupplierDetailView : ContentPage
{
	private readonly SupplierDetailViewModel _supplierDetailViewModel;
	public SupplierDetailView(SupplierDetailViewModel supplierDetailViewModel)
	{
		InitializeComponent();
		BindingContext = _supplierDetailViewModel = supplierDetailViewModel;
	}

	private async void GoBack()
	{
		await Shell.Current.GoToAsync("..");
		_supplierDetailViewModel.ClearDataCommand.Execute(null);
	}

	public string SupplierId
	{
		set
		{
			if (value != null)
			{
				_supplierDetailViewModel.GetDataCommand.Execute(value);
			}
			else
			{
				Task.Run(GoBack);
			}
		}
	}

	private async void DeleteItem(object sender, EventArgs e)
	{
		string namaItem = _supplierDetailViewModel.Supplier!.Nama!;
		bool answer = await DisplayAlert("Konfirmasi", $"Yakin ingin menghapus {namaItem}?", "Ya", "Tidak");

		if (answer == true)
		{
			_supplierDetailViewModel.DeleteDataCommand.Execute(null);
		}
	}
}