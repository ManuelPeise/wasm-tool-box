using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Web.Core.Client.Shared.Components.AppBar
{
    public partial class AppBar
    {
   
        [Parameter]
        public EventCallback<int> LogoutCallback { get; set; }

        [Parameter]
        public string PageTitle { get; set; }

        public string? UserIdentifier { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            UserIdentifier = await GetEmailAdress();
        }

        protected async Task OnLogout(EventArgs e)
        {
            var userId = await GetUserId();

            await LogoutCallback.InvokeAsync(userId);
        }

        private async Task<string?> GetEmailAdress()
        {
            var auth = await AuthenticationStateTask;

            return auth.User.Identity?.Name ?? "";
        }

        private async Task<int> GetUserId()
        {
            var auth = await AuthenticationStateTask;

            var identifierClaimValue = auth.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrWhiteSpace(identifierClaimValue))
            {
                return int.Parse(identifierClaimValue);
            }

            return -1;
        }
    }
}
