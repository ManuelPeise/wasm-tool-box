namespace Data.Models.Entities.User
{
    public class UserRoleEntity: AEntity
    {
        public int? AppuserId { get; set; }
        public virtual AppUserEntity? Appuser { get; set; } = null;
        public int? RoleId { get; set; }
        public virtual RoleEntity? Role { get; set; } = null;
    }
}
