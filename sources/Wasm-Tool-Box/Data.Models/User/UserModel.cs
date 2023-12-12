using Shared.Enums.User;
using Shared.Interfaces.User;

namespace Data.Models.User
{
    public class UserModel : IUser
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRoleEnum UserRole { get; set; }
    }
}
