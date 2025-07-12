using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.ViewModels.Masterdata.Pelanggan;

namespace inovasyposmobile.Views.Pages.Masterdata.Pelanggan;

public partial class PelangganView : ContentPage
{
	private readonly PelangganListViewModel _pelangganListViewModel;
	public PelangganView(PelangganListViewModel pelangganListViewModel)
	{
		InitializeComponent();
		BindingContext = _pelangganListViewModel = pelangganListViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_pelangganListViewModel.ClearDataCommand.Execute(null);
		_pelangganListViewModel.GetPelangganCommand.Execute(null);
	}

	private async void GoToDetail(object sender, TappedEventArgs e)
	{
		// foreach (var item in PelangganCollection.ItemsSource)
		// {
		// 	if (PelangganCollection.ItemTemplate.CreateContent() is ViewCell cell && cell.View is VerticalStackLayout border)
		// 	{
		// 		border.BackgroundColor = Colors.Transparent;
		// 	}
		// }

		Console.WriteLine("TAPPED");

		// Highlight selected item
		if (sender is VerticalStackLayout tappedFrame)
		{
			var selectedItem = (PelangganModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync($"PelangganDetail?PelangganId={selectedItem.PelangganId}");
		}
    }

	private async void GoToCreate(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("PelangganCreate");
    }
}