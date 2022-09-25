using Application.Events;
using Application.MessageBrokers;
using Domain.Core.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RawRabbit;
using RawRabbit.Configuration.Exchange;

namespace Infrastructure.MessageBrokers.RabbitMq;

public class RabbitMqListener : IEventListener
{
    private readonly IBusClient _busClient;
    private readonly IServiceScopeFactory _serviceFactory;
    private readonly RabbitMqOptions _options;

    public RabbitMqListener(
        IBusClient busClient,
        IOptions<RabbitMqOptions> options,
        IServiceScopeFactory serviceFactory)
    {
        _busClient = busClient;
        _serviceFactory = serviceFactory;
        _options = options.Value;
    }

    public virtual void Subscribe<TEvent>() where TEvent : IEvent
    {
        Subscribe(typeof(TEvent));
    }

    public virtual void Subscribe(Type type)
    {
        _busClient.SubscribeAsync(
            (Func<IEvent, Task>)(async (msg) =>
            {
                using (var scope = _serviceFactory.CreateScope())
                {
                    var eventBus = scope.ServiceProvider.GetService<IEventBus>();
                    await eventBus.PublishLocal(msg);
                }
            }),
            cfg => cfg.UseSubscribeConfiguration(
                c => c
                .OnDeclaredExchange(GetExchangeDeclaration(type))
                .FromDeclaredQueue(q => q.WithName((_options.Queue.Name ?? AppDomain.CurrentDomain.FriendlyName).Trim().Trim('_') + "_" + type.Name)))
        );
    }


    public virtual async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        if (@event == null)
        {
            throw new ArgumentNullException(nameof(@event), "Event can not be null.");
        }

        await _busClient.PublishAsync(
            @event,
            cfg => cfg.UsePublishConfiguration(
                c => c
                .OnDeclaredExchange(GetExchangeDeclaration<TEvent>())
            )
        );
    }

    public virtual async Task Publish(string message, string type)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentNullException(nameof(message), "Event message can not be null.");
        }

        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentNullException(nameof(type), "Event type can not be null.");
        }

        await _busClient.PublishAsync(
            message,
            cfg => cfg.UsePublishConfiguration(
                c => c
                .OnDeclaredExchange(GetExchangeDeclaration(type))
            )
        );
    }

    private Action<IExchangeDeclarationBuilder> GetExchangeDeclaration<T>()
    {
        return GetExchangeDeclaration(typeof(T));
    }

    private Action<IExchangeDeclarationBuilder> GetExchangeDeclaration(Type type)
    {
        var name = MessageBrokersHelper.GetTypeName(type);

        return GetExchangeDeclaration(name);
    }

    private Action<IExchangeDeclarationBuilder> GetExchangeDeclaration(string name)
    {
        return e => e
            .WithName(_options.Exchange.Name)
            .WithArgument("key", name);
    }
}