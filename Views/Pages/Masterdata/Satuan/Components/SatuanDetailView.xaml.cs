using inovasyposmobile.ViewModels.Masterdata.Satuan;

namespace inovasyposmobile.Views.Pages.Masterdata.Satuan.Components;

[QueryProperty(nameof(SatuanId), "SatuanId")]
public partial class SatuanDetailView : ContentPage
{
	private readonly SatuanDetailViewModel _satuanDetailViewModel;
	public SatuanDetailView(SatuanDetailViewModel satuanDetailViewModel)
	{
		InitializeComponent();
		BindingContext = _satuanDetailViewModel = satuanDetailViewModel;
	}

	private async void GoBack()
	{
		await Shell.Current.GoToAsync("..");
		_satuanDetailViewModel.ClearDataCommand.Execute(null);
	}

	public string SatuanId
	{
		set
		{
			if (value != null)
			{
				_satuanDetailViewModel.GetDataCommand.Execute(value);
			}
			else
			{
				Task.Run(GoBack);
			}
		}
	}

	private async void DeleteItem(object sender, EventArgs e)
	{
		string namaItem = _satuanDetailViewModel.Satuan!.Nama!;
		bool answer = await DisplayAlert("Konfirmasi", $"Yakin ingin menghapus {namaItem}?", "Ya", "Tidak");

		if (answer == true)
		{
			_satuanDetailViewModel.DeleteDataCommand.Execute(null);
		}
	}
}