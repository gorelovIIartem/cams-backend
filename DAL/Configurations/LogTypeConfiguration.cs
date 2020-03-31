using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class LogTypeConfiguration : IEntityTypeConfiguration<LogType>
    {
        public void Configure(EntityTypeBuilder<LogType> builder)
        {
            builder.HasKey(p => p.LogTypeId);
            builder.HasMany(p => p.Logs).WithOne(p => p.LogType).HasForeignKey(p => p.LogTypeId);
        }
    }
}
