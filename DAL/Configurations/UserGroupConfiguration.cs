using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations
{
    internal class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(p => new { p.GroupId, p.UserId });

            builder.HasOne(p => p.User)
                   .WithMany(p => p.UserGroups)
                   .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Group)
                   .WithMany(p => p.UserGroups)
                   .HasForeignKey(p => p.GroupId);
        }
    }
}
