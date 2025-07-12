namespace inovasyposmobile.Views.Controls;

public partial class SwitchColumnControl : ContentView
{
	public SwitchColumnControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(SwitchColumnControl), string.Empty, propertyChanged: OnPlaceholderChanged);
	public string Title
	{
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}
	private static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (SwitchColumnControl)bindable;
		control.TitleComponent.Text = (string)newValue;
	}

	public static readonly BindableProperty IsToggledProperty =
			BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(SwitchColumnControl), false, defaultBindingMode: BindingMode.TwoWay);
	public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }
}