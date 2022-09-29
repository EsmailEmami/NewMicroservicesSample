using Domain.Core.Entities;

namespace Domain.Core.Events.EfCore;

public interface IEfCoreEntity<TPrimaryKey> : IEntity<TPrimaryKey>
{
}