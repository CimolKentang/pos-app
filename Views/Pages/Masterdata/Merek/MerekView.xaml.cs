using inovasyposmobile.Models.Masterdata;
using inovasyposmobile.ViewModels.Masterdata.Merek;

namespace inovasyposmobile.Views.Pages.Masterdata.Merek;

public partial class MerekView : ContentPage
{
	private readonly MerekListViewModel _merekListViewModel;
	public MerekView(MerekListViewModel merekListViewModel)
	{
		InitializeComponent();
		BindingContext = _merekListViewModel = merekListViewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		_merekListViewModel.ClearDataCommand.Execute(null);
		_merekListViewModel.GetDataCommand.Execute(null);
	}

	private async void GoToDetail(object sender, TappedEventArgs e)
	{
		foreach (var item in MerekCollection.ItemsSource)
		{
			if (MerekCollection.ItemTemplate.CreateContent() is ViewCell cell && cell.View is VerticalStackLayout border)
			{
				border.BackgroundColor = Colors.Transparent;
			}
		}

		// Highlight selected item
		if (sender is VerticalStackLayout tappedFrame)
		{
			var selectedItem = (MerekModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync($"MerekDetail?MerekId={selectedItem.MerekId}");
		}
    }

	private async void GoToCreate(object sender, TappedEventArgs e)
	{
		await Shell.Current.GoToAsync("MerekCreate");
    }
}