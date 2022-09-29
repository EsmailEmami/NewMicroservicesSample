using Domain.Core.Entities;
using Domain.Core.Events.EfCore;

namespace Application.Core.Repositories.EfCore;

public interface IEfCoreRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEfCoreEntity<TPrimaryKey>
{
    TPrimaryKey InsertAndGetId(TEntity entity);
    Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);
}

public interface IEfCoreRepository<TEntity> : IEfCoreRepository<TEntity, int> where TEntity : class, IEfCoreEntity<int>
{
}