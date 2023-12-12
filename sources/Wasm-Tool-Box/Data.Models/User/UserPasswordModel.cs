namespace Data.Models.User
{
    public class UserPasswordModel
    {
        public int UserId { get; set; }
        public int CredentialId { get; set; }
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
