using CommunityToolkit.Maui.Views;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.ViewModels;

namespace inovasyposmobile.Views.Controls;

public partial class SelectListOption : Popup
{
	private readonly ValueDisplayViewModel _valueDisplayView;
	public SelectListOption(ValueDisplayViewModel valueDisplayView, string selectMultipleFor, string itemName)
	{
		InitializeComponent();
		BindingContext = _valueDisplayView = valueDisplayView;

		_valueDisplayView.SelectMultipleFor = selectMultipleFor;

		// height and width relative to device screen
		var screen = DeviceDisplay.MainDisplayInfo;
		double screenWidth = screen.Width / screen.Density;
		double screenHeight = screen.Height / screen.Density;

		Container.HeightRequest = screenHeight * 0.6;
		Container.WidthRequest = screenWidth * 0.9;

		Title.Text = $"Pilih {itemName}";
		SearchComponent.Placeholder = $"Cari {itemName}";

		LoadData();
	}

	private void LoadData()
	{
		_valueDisplayView.GetDatasCommand.Execute(null);
	}

	public void CloseCommand(object sender, EventArgs e)
	{
		Close(false);
	}

	private void OnItemTapped(object sender, EventArgs e)
	{
		// Reset all items' background color first
		foreach (var item in MyCollectionView.ItemsSource)
		{
			if (MyCollectionView.ItemTemplate.CreateContent() is ViewCell cell && cell.View is Border border)
			{
				border.Stroke = Colors.LightGray;
            	border.BackgroundColor = Colors.Transparent;
			}
		}

		// Highlight selected item
		if (sender is Border tappedFrame)
		{
			var selectedItem = (ValueDisplayFilterModel)tappedFrame.BindingContext;
			Close(selectedItem);
		}
	}
}