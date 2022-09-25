using Domain.Core.Entities;

namespace Application.Core.Repositories.Mongo;

public interface IMongoRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
}

public interface IMongoRepository<TEntity> : IMongoRepository<TEntity, int> where TEntity : class, IEntity<int>
{
}