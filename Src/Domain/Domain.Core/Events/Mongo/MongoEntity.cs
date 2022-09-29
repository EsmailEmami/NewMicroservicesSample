using Domain.Core.Entities;

namespace Domain.Core.Events.Mongo;

public abstract class MongoEntity<TPrimaryKey> : Entity<TPrimaryKey>, IMongoEntity<TPrimaryKey>
{
}

public abstract class MongoEntity : MongoEntity<int>, IMongoEntity<int>
{
}