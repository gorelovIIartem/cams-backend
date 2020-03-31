using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class DeviceGroupConfiguration : IEntityTypeConfiguration<DeviceGroup>
    {
        public void Configure(EntityTypeBuilder<DeviceGroup> builder)
        {
            builder.HasKey(p => p.DeviceGroupId);
        }
    }
}
