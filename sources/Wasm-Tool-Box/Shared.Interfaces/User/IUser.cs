using Shared.Enums.User;
using System.ComponentModel.DataAnnotations;

namespace Shared.Interfaces.User
{
    public interface IUser
    {
        [DataType(DataType.EmailAddress)] 
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public UserRoleEnum UserRole { get; set; }
    }
}
