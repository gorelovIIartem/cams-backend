using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class DeviceRuleConfiguration : IEntityTypeConfiguration<DeviceRule>
    {
        public void Configure(EntityTypeBuilder<DeviceRule> builder)
        {
            builder.HasKey(p => new { p.DeviceId, p.RuleId });

            builder.HasOne(p => p.Rule)
                   .WithMany(p => p.DeviceRules)
                   .HasForeignKey(p => p.RuleId);

            builder.HasOne(p => p.Device)
                   .WithMany(p => p.DeviceRules)
                   .HasForeignKey(p => p.DeviceId);
        }
    }
}
