using Data.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums.User;

namespace Data.Database.DataSeeds
{
    public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            var roles = Enum.GetNames(typeof(UserRoleEnum));

            var id = 1;
            var timeStamp = DateTime.UtcNow;

            foreach (var role in roles)
            {
                builder.HasData(new RoleEntity
                {
                    Id = id,
                    Name = role,
                    Description = $"Role: {role}",
                    CreatedBy = "System",
                    CreatedAt = timeStamp,
                    UpdatedBy = "System",
                    UpdatedAt = timeStamp,
                });

                id++;
            }
        }
    }
}
