using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configurations.IdentityRoleConfiguration;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasKey(x => new { x.IdentityId, x.RoleId });

        builder.Ignore(x => x.Id);
        builder.Ignore(x => x.Deleted);

        builder.HasOne(x => x.Identity)
            .WithMany(x => x.IdentityRole)
            .HasForeignKey(x => x.IdentityId);
        builder.HasOne(x => x.Role)
            .WithMany(x => x.IdentityRole)
            .HasForeignKey(x => x.RoleId);
    }
}