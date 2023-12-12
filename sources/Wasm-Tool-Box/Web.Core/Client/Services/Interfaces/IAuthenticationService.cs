using Data.Models.User;

namespace Web.Core.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Login(UserLoginModel loginModel);
        Task Logout(int userId);
    }
}
