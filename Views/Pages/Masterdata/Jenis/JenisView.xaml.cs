using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.ViewModels.Masterdata.Jenis;

namespace inovasyposmobile.Views.Pages.Masterdata.Jenis;

public partial class JenisView : ContentPage
{
	private readonly JenisListViewModel _jenisViewModel;
	public JenisView(JenisListViewModel jenisListViewModel)
	{
		InitializeComponent();
		BindingContext = _jenisViewModel = jenisListViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_jenisViewModel.ClearDataCommand.Execute(null);
		_jenisViewModel.GetJenisCommand.Execute(null);
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
			var selectedItem = (JenisModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync($"JenisDetail?JenisId={selectedItem.JenisId}");
		}
    }

	private async void GoToCreate(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("JenisCreate");
    }
}