using Data.Models.User;

namespace Web.Core.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Login(UserLoginModel loginModel);
        Task<bool> Logout(int userId);
    }
}
