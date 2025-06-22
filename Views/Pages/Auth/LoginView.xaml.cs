using Android.Content;
using Android.Views.InputMethods;
using inovasyposmobile.ViewModels.Auth;

namespace inovasyposmobile.Views.Pages.Auth;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
	}

	private void UsernameCompleted(object sender, EventArgs e)
	{
		PasswordEntry.Focus();
	}

	private void UnfocusAllEntry(object sender, EventArgs e)
	{
		var context = Platform.CurrentActivity;
        var inputMethodManager = context?.GetSystemService(Context.InputMethodService) as InputMethodManager;

        if (inputMethodManager != null)
        {
            var activity = Platform.CurrentActivity;
            var token = activity?.CurrentFocus?.WindowToken;
            inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
            activity?.Window?.DecorView.ClearFocus();
        }
		
		// UsernameEntry.Unfocus();
		// PasswordEntry.Unfocus();
	}
}