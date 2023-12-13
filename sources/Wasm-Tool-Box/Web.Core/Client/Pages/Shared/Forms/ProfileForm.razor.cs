using Data.Models.User;
using Microsoft.AspNetCore.Components;
using Web.Core.Client.Services.Interfaces;

namespace Web.Core.Client.Pages.Shared.Forms
{
    public partial class ProfileForm : IDisposable
    {
        private bool _userDataButtonsDisabled = true;
        private bool _passwordButtonsDisabled = true;
        [Inject]
        private IUserService UserService { get; set; }
        [Inject]
        private IValidationService<UserPasswordModel> PasswordValidationService { get; set; }
        [Inject]
        public IValidationService<UserDataModel> UserDataValidationService { get; set; }
        [Parameter]
        public int UserId { get; set; }
        [Parameter]
        public UserDataModel Profile { get; set; } = new UserDataModel();

        protected override void OnInitialized()
        {
            UserDataValidationService.ValidationModel = Profile;
            UserDataValidationService.OriginalState = new UserDataModel
            {
                UserId = UserId,
                FirstName = Profile.FirstName,
                LastName = Profile.LastName,
                Email = Profile.Email,
                Password = Profile.Password,
                ConfirmPassword = Profile.ConfirmPassword,
            };

            UserDataValidationService.OnChange += StateHasChanged;

            PasswordValidationService.ValidationModel = new UserPasswordModel { UserId = UserId };
            PasswordValidationService.OriginalState = new UserPasswordModel { UserId = UserId };
            PasswordValidationService.OnChange += StateHasChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            UserDataValidationService.OnValidate(out _userDataButtonsDisabled);
            PasswordValidationService.OnValidate(out _passwordButtonsDisabled);
        }

        private void ResetUserData()
        {
            UserDataValidationService.ResetChanges();
            _userDataButtonsDisabled = true;
        }

        private async Task SubmitUserData()
        {
            await UserService.UpdateUserData(UserDataValidationService.ValidationModel);

            _userDataButtonsDisabled = true;
        }

        private void ResetPasswordData()
        {
            PasswordValidationService.ResetChanges();
            _passwordButtonsDisabled = true;
        }

        private async Task SubmitPasswordData()
        {
            await UserService.UpdatePassword(PasswordValidationService.ValidationModel);

            _passwordButtonsDisabled = true;
        }

        public void Dispose()
        {
            UserDataValidationService.OnChange -= StateHasChanged;
            PasswordValidationService.OnChange -= StateHasChanged;
        }
    }
}
