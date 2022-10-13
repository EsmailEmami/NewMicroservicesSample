using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Commands;

public abstract class CommandHandler
{
    public void CheckValidation<TCommand>(Command<TCommand> command)
    {
        if (!command.IsValid())
            throw new ValidationException(string.Join(", ", command.ValidationResult.Errors));
    }
}
