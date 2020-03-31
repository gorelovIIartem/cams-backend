using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Devices)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId);

            builder.HasMany(p => p.Groups)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId);

            builder.HasMany(p => p.Rules)
                   .WithOne(p => p.User)
                   .HasForeignKey(p => p.UserId);
        }
    }
}
