using Application.MessageBrokers;
using Application.Outbox.Stores;
using Domain.Core.Events;
using Newtonsoft.Json;

namespace Application.Outbox;

public class OutboxListener : IOutboxListener
{
    private readonly IOutboxStore _store;

    public OutboxListener(IOutboxStore store)
    {
        _store = store;
    }

    public virtual async Task Commit(OutboxMessage message)
    {
        await _store.Add(message);
    }

    public virtual async Task Commit<TEvent>(TEvent @event) where TEvent : IEvent
    {
        var outboxMessage = new OutboxMessage();
        string data = @event == null ? "{}" : JsonConvert.SerializeObject(@event, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        outboxMessage.CreateMessage(MessageBrokersHelper.GetTypeName<TEvent>(), data);

        await Commit(outboxMessage);
    }
}