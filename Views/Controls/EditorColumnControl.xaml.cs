namespace inovasyposmobile.Views.Controls;

public partial class EditorColumnControl : ContentView
{
	public EditorColumnControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(EditorColumnControl), string.Empty, propertyChanged: OnPlaceholderChanged);
	public string Placeholder
	{
		get => (string)GetValue(PlaceholderProperty);
		set => SetValue(PlaceholderProperty, value);
	}
	private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (EditorColumnControl)bindable;
		control.TextComponent.Placeholder = (string)newValue;
	}

	public static readonly BindableProperty IsRequiredProperty =
			BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(EditorColumnControl), false);
	public bool IsRequired
    {
        get => (bool)GetValue(IsRequiredProperty);
        set => SetValue(IsRequiredProperty, value);
    }
	
	public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(EditorColumnControl), string.Empty);
	public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}