using DAL.Configurations;
using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.ApplicationDbContext
{
    public sealed class CamsDbContext : IdentityDbContext<User>
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<DeviceLog> DeviceLogs { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceGroup> DeviceGroups { get; set; }
        public DbSet<DeviceRule> DeviceRules { get; set; }
        public DbSet<LogType> LogTypes { get; set; }
        public DbSet<Rule> Rules { get; set; }

        public CamsDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public bool DatabaseExists()
            => (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DeviceConfiguration());
            builder.ApplyConfiguration(new DeviceGroupConfiguration());
            builder.ApplyConfiguration(new DeviceLogConfiguration());
            builder.ApplyConfiguration(new DeviceRuleConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new LogTypeConfiguration());
            builder.ApplyConfiguration(new RuleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserGroupConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
