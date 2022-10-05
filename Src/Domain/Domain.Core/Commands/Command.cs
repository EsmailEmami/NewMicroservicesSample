using FluentValidation.Results;

namespace Domain.Core.Commands;

public abstract class Command<TResponse> : ICommand<TResponse>
{
    public DateTime Timestamp => DateTime.Now;
    public ValidationResult ValidationResult { get; set; }

    public abstract bool IsValid();
}

public abstract class Command : ICommand
{
    protected Command(DateTime timestamp)
    {
        Timestamp = timestamp;
    }

    public DateTime Timestamp { get; }
    public ValidationResult ValidationResult { get; set; }

    public abstract bool IsValid();
}