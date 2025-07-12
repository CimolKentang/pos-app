using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.ViewModels.Masterdata.Satuan;

namespace inovasyposmobile.Views.Pages.Masterdata.Satuan;

public partial class SatuanView : ContentPage
{
	private readonly SatuanListViewModel _satuanListViewModel;
	public SatuanView(SatuanListViewModel satuanListViewModel)
	{
		InitializeComponent();
		BindingContext = _satuanListViewModel = satuanListViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_satuanListViewModel.ClearDataCommand.Execute(null);
		_satuanListViewModel.GetDataCommand.Execute(null);
	}

	private async void GoToDetail(object sender, TappedEventArgs e)
	{
		foreach (var item in SatuanCollection.ItemsSource)
		{
			if (SatuanCollection.ItemTemplate.CreateContent() is ViewCell cell && cell.View is VerticalStackLayout border)
			{
				border.BackgroundColor = Colors.Transparent;
			}
		}

		// Highlight selected item
		if (sender is VerticalStackLayout tappedFrame)
		{
			var selectedItem = (SatuanModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync($"SatuanDetail?SatuanId={selectedItem.SatuanId}");
		}
    }

	private async void GoToCreate(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("SatuanCreate");
    }
}