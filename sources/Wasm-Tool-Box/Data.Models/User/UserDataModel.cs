namespace Data.Models.User
{
    public class UserDataModel
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public int FailedLogins { get; set; }
    }
}
