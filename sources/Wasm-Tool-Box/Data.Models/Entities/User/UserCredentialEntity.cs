namespace Data.Models.Entities.User
{
    public class UserCredentialEntity: AEntity
    {
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public DateTime ExpieresAt { get; set; }
        public string? Token { get; set; }
     
    }
}
