using DAL.Helpers.Enums;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.Configurations
{
    internal class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(p => p.DeviceId);
            builder.Property(p => p.DeviceType).HasConversion(p => p.ToString(), p => (DeviceType)Enum.Parse(typeof(DeviceType), p));
            builder.HasMany(p => p.DeviceLogs).WithOne(p => p.Device).HasForeignKey(p => p.LogId);
        }
    }
}
