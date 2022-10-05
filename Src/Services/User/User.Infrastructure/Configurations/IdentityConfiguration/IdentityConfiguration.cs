using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infrastructure.Configurations.IdentityConfiguration;

public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x=> x.Password)
            .IsRequired()
            .HasMaxLength(460);

        builder.HasMany(x => x.IdentityRole).WithOne(x => x.Identity)
            .HasForeignKey(x => x.IdentityId);

        builder.HasOne(x => x.User).WithOne(x => x.Identiy)
            .HasForeignKey<Domain.Entities.User>(x => x.IdentityId);
    }
}