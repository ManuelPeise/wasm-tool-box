using Microsoft.AspNetCore.Components;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public IAuthenticationService AuthService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }

        public async Task OnLogout(int userId)
        {
            if (userId != -1)
            {
                if(await AuthService.Logout(userId))
                {
                    NavManager.NavigateTo("/", true);
                }
            }
        }
    }
}
