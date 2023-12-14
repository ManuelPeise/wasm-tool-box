using Data.Database.DataSeeds;
using Data.Models.Entities.Log;
using Data.Models.Entities.Settings;
using Data.Models.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Data.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> opt, IConfiguration config) : base(opt)
        {
            _configuration = config;
        }

        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<SettingsEntity> Settings { get; set; }

        public DbSet<AppUserEntity> AppUsers { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserCredentialEntity> Credentials { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CredentialsConfiguration(_configuration));
            modelBuilder.ApplyConfiguration(new UserConfiguration(_configuration));
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
