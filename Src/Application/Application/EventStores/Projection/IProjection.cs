using Domain.Core.Events;

namespace Application.EventStores.Projection;

public interface IProjection
{
    Type[] Handles { get; }
    void Handle(IEvent @event);
}