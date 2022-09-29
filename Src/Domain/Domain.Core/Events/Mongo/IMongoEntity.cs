using Domain.Core.Entities;

namespace Domain.Core.Events.Mongo;

public interface IMongoEntity<TPrimaryKey> : IEntity<TPrimaryKey>
{
}