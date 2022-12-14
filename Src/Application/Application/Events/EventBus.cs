using Application.Common;
using Application.EventStores;
using Application.MessageBrokers;
using Application.Outbox;
using Domain.Core.Events;
using MediatR;

namespace Application.Events;

public class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly IOutboxListener _outboxListener;
    private readonly IEventListener _eventListener;

    public EventBus(IMediator mediator, IOutboxListener outboxListener, IEventListener eventListener)
    {
        _mediator = mediator ?? throw new Exception($"Missing dependency '{nameof(IMediator)}'");
        _outboxListener = outboxListener ?? throw new Exception($"Missing dependency '{nameof(IOutboxListener)}'");
        _eventListener = eventListener ?? throw new Exception($"Missing dependency '{nameof(IEventListener)}'");
    }

    public virtual async Task PublishLocal(params IEvent[] events)
    {
        foreach (var @event in events)
        {
            await _mediator.Publish(@event);
        }
    }

    public virtual async Task Commit(params IEvent[] events)
    {
        foreach (var @event in events)
        {
            await SendToMessageBroker(@event);
        }
    }

    public virtual async Task Commit(StreamState stream)
    {
        if (_outboxListener != null)
        {
            var message = Mapping.Map<StreamState, OutboxMessage>(stream);
            await _outboxListener.Commit(message);
        }
        else if (_eventListener != null)
        {
            await _eventListener.Publish(stream.Data, stream.Type);
        }
        else
        {
            throw new ArgumentNullException("No event listener found");
        }
    }

    private async Task SendToMessageBroker(IEvent @event)
    {
        try
        {
            await _outboxListener.Commit(@event);
            await _eventListener.Publish(@event);
        }
        catch (Exception e)
        {
            throw new ArgumentNullException("No event listener found");
        }
    }
}