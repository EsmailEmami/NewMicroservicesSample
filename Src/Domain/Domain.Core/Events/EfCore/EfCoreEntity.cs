using Domain.Core.Entities;

namespace Domain.Core.Events.EfCore;

public abstract class EfCoreEntity<TPrimaryKey> : Entity<TPrimaryKey>, IEfCoreEntity<TPrimaryKey>
{
}

public abstract class EfCoreEntity : EfCoreEntity<int>, IEfCoreEntity<int>
{
}