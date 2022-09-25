using MediatR;

namespace Domain.Core.Events;

public interface IEventHandler<in T> : INotificationHandler<T> where T : IEvent
{ }