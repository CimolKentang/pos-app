using inovasyposmobile.Services.Implementations.Auth;

namespace inovasyposmobile;

public partial class App : Application
{
	private readonly AuthService _authService;
	public App(AuthService authService)
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