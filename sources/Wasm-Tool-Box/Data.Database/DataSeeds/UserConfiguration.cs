using Data.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Data.Database.DataSeeds
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUserEntity>
    {
        private readonly IConfiguration _config;

        public UserConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public void Configure(EntityTypeBuilder<AppUserEntity> builder)
        {
            var timeStamp = DateTime.UtcNow;

            var defaultUser = new AppUserEntity
            {
                Id = 1,
                FailedLogins = 0,
                IsActive = true,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = _config["user:email"],
                CreatedBy = "System",
                CreatedAt = timeStamp,
                UpdatedBy = "System",
                UpdatedAt = timeStamp,
                UserRoles = null,
                Credentials = null,
                CredentialsId = 1
            };

            builder.HasData(defaultUser);
        }
    }
}
