using Data.Models.Entities.Log;
using Data.Models.Entities.Settings;
using Data.Models.Family;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shared.Enums.Settings;

namespace Data.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DatabaseContext(DbContextOptions<DatabaseContext> opt, IConfiguration config) : base(opt)
        {
            _configuration = config;
        }

        public DbSet<LogEntity> LogTable { get; set; }
        public DbSet<SettingsEntity> SettingsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var family = new FamilyModel
            {
                FamilyId = new Guid(_configuration["family:id"]),
                Name = _configuration["family:name"],
                MembersCount = int.Parse(_configuration["family:membersCount"])
            };

            modelBuilder.Entity<SettingsEntity>().HasData(new SettingsEntity
            {
                Id = 1,
                SettingsType = SettingsTypeEnum.FamilySettings,
                JsonValue = JsonConvert.SerializeObject(family),
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = null,
                UpdatedAt = null
            });
        }
    }
}
