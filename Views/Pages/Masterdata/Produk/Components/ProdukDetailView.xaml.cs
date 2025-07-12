using inovasyposmobile.ViewModels.Masterdata.Produk;

namespace inovasyposmobile.Views.Pages.Masterdata.Produk.Components;

[QueryProperty(nameof(ProdukId), "ProdukId")]
public partial class ProdukDetailView : ContentPage
{
	private readonly ProdukDetailViewModel _produkDetailViewModel;
	public ProdukDetailView(ProdukDetailViewModel produkDetailViewModel)
	{
		InitializeComponent();
		BindingContext = _produkDetailViewModel = produkDetailViewModel;
	}

	private async void GoBack()
	{
		await Shell.Current.GoToAsync("..");
		_produkDetailViewModel.ClearDataCommand.Execute(null);
	}

	public string ProdukId
	{
		set
		{
			if (value != null)
			{
				_produkDetailViewModel.GetDataCommand.Execute(value);
			}
			else
			{
				Task.Run(GoBack);
			}
		}
	}

	private async void DeleteItem(object sender, EventArgs e)
	{
		string namaItem = _produkDetailViewModel.Produk!.Nama!;
		bool answer = await DisplayAlert("Konfirmasi", $"Yakin ingin menghapus {namaItem}?", "Ya", "Tidak");

		if (answer == true)
		{
			_produkDetailViewModel.DeleteDataCommand.Execute(null);
		}
    }
}