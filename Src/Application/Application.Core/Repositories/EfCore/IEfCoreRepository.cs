using Domain.Core.Entities;

namespace Application.Core.Repositories.EfCore;

public interface IEfCoreRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
}

public interface IEfCoreRepository<TEntity> : IEfCoreRepository<TEntity, int> where TEntity : class, IEntity<int>
{
}