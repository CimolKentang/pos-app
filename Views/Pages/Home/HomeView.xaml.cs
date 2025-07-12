using inovasyposmobile.ViewModels.Home;

namespace inovasyposmobile.Views.Pages.Home;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
		BindingContext = new HomeViewModel();
	}

	private async void GoToPage(object sender, TappedEventArgs e)
	{
		foreach (var item in PageList.ItemsSource)
		{
			if (PageList.ItemTemplate.CreateContent() is ViewCell cell && cell.View is Grid listItem)
			{
				listItem.BackgroundColor = Colors.Transparent;
			}
		}

		if (sender is VerticalStackLayout tappedFrame)
		{
			var selectedItem = (PageListModel)tappedFrame.BindingContext;
			await Shell.Current.GoToAsync(selectedItem.Page);
		}
    }
}