using Infrastructure.Context;
using Infrastructure.Databases.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infrastructure.Configurations.UserConfiguration;

namespace User.Infrastructure.Context;

public class UserDbContext : EfCoreMainDbContext<UserDbContext>
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.RegisterAllEntities(typeof(Domain.Entities.User).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}