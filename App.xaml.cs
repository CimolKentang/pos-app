using inovasyposmobile.Services.Interfaces.Auth;

namespace inovasyposmobile;

public partial class App : Application
{
	private readonly IAuthService _authService;
	public App(IAuthService authService)
	{
		InitializeComponent();
		_authService = authService;
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		var shell = new AppShell();

		Task.Run(async () =>
		{
			var isAuthenticated = await _authService.IsAuthenticatedAsync();
			await Shell.Current.GoToAsync($"//{(isAuthenticated ? "MainRoute" : "LoginRoute")}");
		}).ConfigureAwait(false);

		return new Window(shell);
	}
}