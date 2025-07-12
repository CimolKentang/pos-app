namespace inovasyposmobile.Views.Controls;

public partial class DetailComponentControl : ContentView
{
	public DetailComponentControl()
	{
		InitializeComponent();
	}

	public static readonly BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(DetailComponentControl), string.Empty, propertyChanged: OnTitleChanged);
	public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
	private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (DetailComponentControl)bindable;
		control.TitleComponent.Text = (string)newValue;
	}

	public static readonly BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(DetailComponentControl), "-", propertyChanged: OnTextChanged);
	public string Text
	{
		get => (string)GetValue(TextProperty);
		set
		{
			SetValue(TextProperty, value);
		}
	}
	private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (DetailComponentControl)bindable;
		if (string.IsNullOrEmpty((string)newValue))
		{
			control.TextComponent.Text = "-";
		}
		else
		{
			control.TextComponent.Text = (string)newValue;
		}
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		Console.WriteLine(Title);
		Console.WriteLine(Text);
	}
}