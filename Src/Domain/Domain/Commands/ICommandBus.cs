﻿using Domain.Core.Commands;

namespace Domain.Commands;

public interface ICommandBus
{
    Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);

    Task Send(ICommand command, CancellationToken cancellationToken = default);
}