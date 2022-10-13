using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Events.Mongo;

namespace Application.Core.Repositories.Mongo;

public interface IMongoRepository<TEntity, in TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IMongoEntity<TPrimaryKey>, IAggregateRoot
{
}

public interface IMongoRepository<TEntity> : IMongoRepository<TEntity, int> where TEntity : class, IMongoEntity<int>, IAggregateRoot
{
}