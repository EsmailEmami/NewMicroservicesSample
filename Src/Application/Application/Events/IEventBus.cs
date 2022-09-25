using Application.EventStores;
using Domain.Core.Events;

namespace Application.Events;

public interface IEventBus
{
    Task PublishLocal(params IEvent[] events);
    Task Commit(params IEvent[] events);
    Task Commit(StreamState stream);
}