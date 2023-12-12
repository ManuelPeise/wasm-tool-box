using Data.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Data.Database.DataSeeds
{
    public class CredentialsConfiguration: IEntityTypeConfiguration<UserCredentialEntity>
    {
        private readonly IConfiguration _config;

        public CredentialsConfiguration(IConfiguration config)
        {
            _config = config;
        }

        public void Configure(EntityTypeBuilder<UserCredentialEntity> builder)
        {
            var timeStamp = DateTime.UtcNow;

            var credentials = new UserCredentialEntity
            {
                Id = 1,
                Password = GetHashedPassword(_config["user:password"], _config["user:salt"]),
                Salt = _config["user:salt"],
                CreatedBy = "System",
                CreatedAt = timeStamp,
                UpdatedBy = "System",
                UpdatedAt = timeStamp,
                ExpieresAt = timeStamp.AddDays(90),
                Token = string.Empty,
            };

            builder.HasData(credentials);
        }

        private string? GetHashedPassword(string? password, string? salt)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(salt))
            {
                return null;
            }

            var bytes = Encoding.ASCII.GetBytes(password).ToList();
            bytes.AddRange(Encoding.ASCII.GetBytes(salt));

            return Convert.ToBase64String(bytes.ToArray());
        }
    }
}
