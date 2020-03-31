using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class DeviceLogConfiguration : IEntityTypeConfiguration<DeviceLog>
    {
        public void Configure(EntityTypeBuilder<DeviceLog> builder)
        {
            builder.HasKey(p => p.LogId);
        }
    }
}
