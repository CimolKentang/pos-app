using inovasyposmobile.ViewModels.Masterdata.Merek;

namespace inovasyposmobile.Views.Pages.Masterdata.Merek.Components;

[QueryProperty(nameof(MerekId), "MerekId")]
public partial class MerekDetailView : ContentPage
{
	private readonly MerekDetailViewModel _merekDetailViewModel;
	public MerekDetailView(MerekDetailViewModel merekDetailViewModel)
	{
		InitializeComponent();
		BindingContext = _merekDetailViewModel = merekDetailViewModel;
	}

	private async void GoBack()
	{
		await Shell.Current.GoToAsync("..");
		_merekDetailViewModel.ClearDataCommand.Execute(null);
	}

	public string MerekId
	{
		set
		{
			if (value != null)
			{
				_merekDetailViewModel.GetDataCommand.Execute(value);
			}
			else
			{
				Task.Run(GoBack);
			}
		}
	}

	private async void DeleteItem(object sender, EventArgs e)
	{
		string namaItem = _merekDetailViewModel.Merek!.Nama!;
		bool answer = await DisplayAlert("Konfirmasi", $"Yakin ingin menghapus {namaItem}?", "Ya", "Tidak");

		if (answer == true)
		{
			_merekDetailViewModel.DeleteDataCommand.Execute(null);
		}
	}
}