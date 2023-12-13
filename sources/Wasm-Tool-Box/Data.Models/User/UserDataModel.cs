using System.ComponentModel.DataAnnotations;

namespace Data.Models.User
{
    public class UserDataModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "[Vorname] muss ausgefüllt werden!")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "[Name] muss ausgefüllt werden!")]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public int FailedLogins { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
