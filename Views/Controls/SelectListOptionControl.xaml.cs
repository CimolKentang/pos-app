using System.Windows.Input;

namespace inovasyposmobile.Views.Controls;

public partial class SelectListOptionControl : ContentView
{
	public SelectListOptionControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(SelectListOptionControl), string.Empty, propertyChanged: OnTitleChanged);
	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}
	private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (SelectListOptionControl)bindable;

		if (!string.IsNullOrEmpty((string)newValue))
		{
			control.TitleComponent.Text = (string)newValue;
		}
		else
		{
			control.TitleComponent.Text = $"Pilih {control.Item}";
		}
	}

	public static readonly BindableProperty ItemProperty =
			BindableProperty.Create(nameof(Item), typeof(string), typeof(SelectListOptionControl), string.Empty, propertyChanged: OnItemChanged);
	public string Item
	{
		get => (string)GetValue(ItemProperty);
		set => SetValue(ItemProperty, value);
	}
	private static void OnItemChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (SelectListOptionControl)bindable;
		control.TitleComponent.Text = $"Pilih {(string)newValue}";
	}
	
	public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SelectListOptionControl), null);
	public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}