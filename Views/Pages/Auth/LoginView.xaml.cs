using inovasyposmobile.ViewModels.Auth;

namespace inovasyposmobile.Views.Pages.Auth;

public partial class LoginView : ContentPage
{
	public LoginView(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
	}
}