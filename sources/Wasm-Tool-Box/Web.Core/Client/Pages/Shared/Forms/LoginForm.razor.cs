using Data.Models.User;
using Microsoft.AspNetCore.Components;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Pages.Shared.Forms
{
    public partial class LoginForm : ComponentBase
    {
        [Parameter]
        public string? RedeirctUri { get; set; }
        [Inject]
        public IAuthenticationService AuthService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UserLoginModel LoginModel { get; set; } = new UserLoginModel();


        private async void OnLogin()
        {
            if (await AuthService.Login(LoginModel))
            {
                NavigationManager.NavigateTo(RedeirctUri ?? "/", true);
            }
        }
    }
}
