using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Commands;

public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    public void CheckValidation<TCommand>(Command<TCommand> command)
    {
        if (!command.IsValid())
            throw new ValidationException(string.Join(", ", command.ValidationResult.Errors));
    }
}
