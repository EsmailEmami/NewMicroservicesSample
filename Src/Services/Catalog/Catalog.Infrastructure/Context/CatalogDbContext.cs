using Catalog.Domain.Entities;
using Catalog.Infrastructure.Configurations.ProductConfiguration;
using Infrastructure.Context;
using Infrastructure.Databases.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Context;

public class CatalogDbContext : EfCoreMainDbContext<CatalogDbContext>
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.RegisterAllEntities(typeof(Product).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}