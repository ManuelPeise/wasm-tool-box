using Data.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Pages.User
{
    public partial class Profile
    {
        private UserDataModel _profileData = new UserDataModel();
        private int _userId;
        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        public IAuthenticationService AuthService { get; set; }
        [Inject]
        public NavigationManager NavManager { get; set; }
        [Inject]
        public IUserService UserService { get; set; }

        public UserDataModel ProfileData { get => _profileData; }
        private bool _isLoading = false;

        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            _userId = await GetUserId();

            if (_userId != -1)
            {
                _profileData = await UserService.GetUserData(_userId);
            }

            await base.OnInitializedAsync();

            _isLoading = false;
        }

        public async Task OnLogout(int userId)
        {
            if (userId != -1)
            {
                if (await AuthService.Logout(userId))
                {
                    NavManager.NavigateTo("/", true);
                }
            }
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
