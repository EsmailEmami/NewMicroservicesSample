using Domain.Core.Events;

namespace Application.EventStores.Aggregate;

public interface IAggregate
{
    Guid Id { get; }
    int Version { get; }
    DateTime CreatedUtc { get; }

    IEnumerable<IEvent> DequeueUncommittedEvents();
}