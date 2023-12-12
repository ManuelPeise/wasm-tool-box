namespace Data.Models.Entities.User
{
    public class AppUserEntity: AEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public int FailedLogins { get; set; }
        public virtual ICollection<UserRoleEntity>? UserRoles { get; set; } = null;
        public int? CredentialsId { get; set; }
        public virtual UserCredentialEntity? Credentials { get; set; } = null;

    }
}
