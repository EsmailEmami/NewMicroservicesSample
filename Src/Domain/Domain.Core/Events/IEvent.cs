using MediatR;

namespace Domain.Core.Events;

public interface IEvent : INotification
{
    Guid Id { get; }
    DateTime CreatedUtc { get; }
}