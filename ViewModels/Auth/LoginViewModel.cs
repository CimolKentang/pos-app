using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Content;
using Android.Views.InputMethods;
using inovasyposmobile.Models.Auth;
using inovasyposmobile.Services.Interfaces.Auth;

namespace inovasyposmobile.ViewModels.Auth
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        private string _username = "";
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password = "";
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private bool _hidePassword = true;
        public bool HidePassword
        {
            get => _hidePassword;
            set
            {
                SetProperty(ref _hidePassword, value);
                OnPropertyChanged(nameof(ShowPassword));
            }
        }

        public bool ShowPassword => !HidePassword;

        public ICommand ToggleHidePasswordCommand { get; }
        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
            ToggleHidePasswordCommand = new Command(ToggleHidePassword);
            LoginCommand = new Command(async () => await Login());
        }

        private void ToggleHidePassword()
        {
            HidePassword = !HidePassword;
        }

        private async Task Login()
        {
            IsLoading = true;

            var context = Platform.CurrentActivity;
            var inputMethodManager = context?.GetSystemService(Context.InputMethodService) as InputMethodManager;

            if (inputMethodManager != null)
            {
                var activity = Platform.CurrentActivity;
                var token = activity?.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);
                activity?.Window?.DecorView.ClearFocus();
            }

            try
            {
                var loginParams = new LoginRequestModel
                {
                    Username = Username,
                    Password = Password
                };

                var token = await _authService.Login(loginParams);

                if (token != null && token.Succeeded)
                {
                    await _authService.SetStorageToken(token.Data!);
                    await Shell.Current.GoToAsync("//MainRoute");
                }
                else
                {
                    Username = "";
                    Password = "";
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}