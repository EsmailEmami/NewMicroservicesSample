using Domain.Core;
using Domain.Core.Events.EfCore;

namespace Application.Core.Repositories.EfCore;

public abstract class EfCoreRepository<TEntity, TPrimaryKey> : Repository<TEntity, TPrimaryKey>, IEfCoreRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEfCoreEntity<TPrimaryKey>, IAggregateRoot
{
    public virtual TPrimaryKey InsertAndGetId(TEntity entity) => Insert(entity).Id;

    public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity) => Task.FromResult(InsertAndGetId(entity));
}

public abstract class EfCoreRepository<TEntity> : EfCoreRepository<TEntity, int> where TEntity : class, IEfCoreEntity<int>, IEfCoreRepository<TEntity>, IAggregateRoot
{
}
