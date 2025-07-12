namespace inovasyposmobile.Views.Controls;

public partial class EntryColumnControl : ContentView
{
	public EntryColumnControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EntryColumnControl), string.Empty, propertyChanged: OnPlaceholderChanged);
	public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }
	private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (EntryColumnControl)bindable;
		control.TextComponent.Placeholder = (string)newValue;
	}

	public static readonly BindableProperty IsRequiredProperty =
			BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(EntryColumnControl), false);
	public bool IsRequired
    {
        get => (bool)GetValue(IsRequiredProperty);
        set => SetValue(IsRequiredProperty, value);
    }

	public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryColumnControl), string.Empty);
	public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}