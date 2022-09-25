using Domain.Core.Events;

namespace Application.EventStores.Projection;

public abstract class Projection : IProjection
{
    private readonly Dictionary<Type, Action<IEvent>> _handlers = new Dictionary<Type, Action<IEvent>>();

    public Type[] Handles => _handlers.Keys.ToArray();

    protected virtual void Projects<TEvent>(Action<IEvent> action)
    {
        _handlers.Add(typeof(IEvent), action);
    }

    public virtual void Handle(IEvent @event)
    {
        _handlers[@event.GetType()](@event);
    }
}