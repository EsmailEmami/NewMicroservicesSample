using Domain.Core.Commands;
using MediatR;

namespace Domain.Commands;

public class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;

    public CommandBus(IMediator mediator)
    {
        _mediator = mediator ?? throw new Exception($"Missing dependency '{nameof(IMediator)}'");
    }

    public virtual async Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default) =>
        await _mediator.Send(command, cancellationToken);
    public virtual async Task Send(ICommand command, CancellationToken cancellationToken = default) =>
        await _mediator.Send(command, cancellationToken);
}