using Data.Models.Entities.User;
using Data.Models.User;

namespace Data.Models.Extensions
{
    public static class UserExtensions
    {
        public static AppUserEntity ToEntity(this UserModel model, int? credentialsId = null)
        {
            return new AppUserEntity
            {
                Email = model.EmailAddress,
                FailedLogins = 0,
                IsActive = true,
                CredentialsId = credentialsId,
            };
        }
    }
}
