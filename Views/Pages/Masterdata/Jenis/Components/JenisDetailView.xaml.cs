using inovasyposmobile.ViewModels.Masterdata.Jenis;

namespace inovasyposmobile.Views.Pages.Masterdata.Jenis.Components;

[QueryProperty(nameof(JenisId), "JenisId")]
public partial class JenisDetailView : ContentPage
{
	private readonly JenisDetailViewModel _jenisDetailViewModel;
	public JenisDetailView(JenisDetailViewModel jenisDetailViewModel)
	{
		InitializeComponent();
		BindingContext = _jenisDetailViewModel = jenisDetailViewModel;
	}

	private async void GoBack()
	{
		await Shell.Current.GoToAsync("..");
		_jenisDetailViewModel.ClearDataCommand.Execute(null);
	}

	public string JenisId
	{
		set
		{
			if (value != null)
			{
				_jenisDetailViewModel.GetDataCommand.Execute(value);
			}
			else
			{
				Task.Run(GoBack);
			}
		}
	}

	private async void DeleteItem(object sender, EventArgs e)
	{
		string namaItem = _jenisDetailViewModel.Jenis!.Nama!;
		bool answer = await DisplayAlert("Konfirmasi", $"Yakin ingin menghapus {namaItem}?", "Ya", "Tidak");

		if (answer == true)
		{
			_jenisDetailViewModel.DeleteDataCommand.Execute(null);
		}
    }
}