using Domain.Core.Events.Mongo;

namespace Application.Core.Repositories.Mongo;

public abstract class MongoRepository<TEntity, TPrimaryKey> : Repository<TEntity, TPrimaryKey>, IMongoRepository<TEntity, TPrimaryKey>
    where TEntity : class, IMongoEntity<TPrimaryKey>
{
}

public abstract class MongoRepository<TEntity> : MongoRepository<TEntity, int> where TEntity : class, IMongoEntity<int>, IMongoRepository<TEntity>
{
}