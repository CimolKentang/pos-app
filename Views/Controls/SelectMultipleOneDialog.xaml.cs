using System.Windows.Input;
using CommunityToolkit.Maui.Views;
using inovasyposmobile.Constants;
using inovasyposmobile.Models.Filters;
using inovasyposmobile.ViewModels;

namespace inovasyposmobile.Views.Controls;

public partial class SelectMultipleOneDialog : Popup
{
	private readonly ValueDisplayViewModel _valueDisplayView;
	private string _selectMultipleFor;
	public SelectMultipleOneDialog(ValueDisplayViewModel valueDisplayView, string selectMultipleFor)
	{
		InitializeComponent();
		BindingContext = _valueDisplayView = valueDisplayView;
		_selectMultipleFor = selectMultipleFor;
		LoadData();
	}

	private void LoadData()
	{
		try
		{
			_valueDisplayView.GetDatasCommand.Execute(_selectMultipleFor);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public void CloseCommand(object sender, EventArgs e)
	{
		Close(false);
	}

	private void OnItemTapped(object sender, EventArgs e)
	{
		// Reset all items' background color first
		foreach (var item in CollectionView.ItemsSource)
		{
			if (CollectionView.ItemTemplate.CreateContent() is ViewCell cell && cell.View is Border border)
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