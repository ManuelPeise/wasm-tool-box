using Data.Models.Entities.User;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.DataSeeds
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            var timeStamp = DateTime.UtcNow;
            
            var role = new UserRoleEntity
            {
                Id = 1,
                AppuserId = 1,
                RoleId = 1,
                CreatedBy = "System",
                CreatedAt = timeStamp,
                UpdatedBy = "System",
                UpdatedAt = timeStamp
            };

            builder.HasData(role);
        }
    }
}
