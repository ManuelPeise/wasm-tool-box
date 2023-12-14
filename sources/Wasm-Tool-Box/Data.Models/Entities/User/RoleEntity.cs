namespace Data.Models.Entities.User
{
    public class RoleEntity: AEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public virtual UserRoleEntity? UserRole { get; set; }
    }
}
