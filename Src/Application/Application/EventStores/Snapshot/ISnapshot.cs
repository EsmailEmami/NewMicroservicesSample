using Application.EventStores.Aggregate;

namespace Application.EventStores.Snapshot;

public interface ISnapshot
{
    Type Handles { get; }
    void Handle(IAggregate aggregate);
}