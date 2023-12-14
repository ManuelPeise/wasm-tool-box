using Data.Models.User;

namespace Web.Core.Client.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDataModel> GetUserData(int userId);
        Task UpdateUserData(UserDataModel userData);
        Task UpdatePassword(UserPasswordModel userPasswordModel);
    }
}
