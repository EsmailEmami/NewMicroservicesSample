using Application.Core;
using Catalog.Infrastructure.Context;
using Domain.Core.Entities;
using Infrastructure.Repositories.EfCore;

namespace Catalog.Infrastructure.Repositories;

public class CatalogMainRepository<TEntity, TPrimaryKey> : EfCoreMainRepository<CatalogDbContext, TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    public CatalogMainRepository(IUnitOfWork dbContextProvider) : base((CatalogDbContext)dbContextProvider)
    {
    }
}

public class CatalogMainRepository<TEntity> : CatalogMainRepository<TEntity, int>
    where TEntity : class, IEntity<int>
{
    public CatalogMainRepository(IUnitOfWork dbContextProvider) : base((CatalogDbContext)dbContextProvider)
    {
    }
}