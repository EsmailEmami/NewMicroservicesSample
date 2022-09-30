using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configurations.UserRoleConfiguration;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(x => new {x.UserId, x.RoleId});

        builder.Ignore(x => x.Id);
        builder.Ignore(x => x.Deleted);

        builder.HasOne(x=> x.User).WithMany(x=> x.UserRoles).HasForeignKey(x=>x.UserId).IsRequired();
        builder.HasOne(x=> x.Role).WithMany(x=> x.UserRoles).HasForeignKey(x=>x.RoleId).IsRequired();
    }
}