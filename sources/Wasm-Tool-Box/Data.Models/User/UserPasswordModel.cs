using System.ComponentModel.DataAnnotations;

namespace Data.Models.User
{
    public class UserPasswordModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Das [aktuelle] Passwort muss eingegeben werden!")]
        public string CurrentPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Das [neue] Passwort muss eingegeben werden!")]
        public string NewPassword { get; set; } = string.Empty;
        [Required(ErrorMessage = "Das [neue] Passwort muss wiederholt werden!")]
        public string ConfirmedPassword { get; set; } = string.Empty;
    }
}
