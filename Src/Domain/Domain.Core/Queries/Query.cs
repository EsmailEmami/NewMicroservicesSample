using FluentValidation.Results;

namespace Domain.Core.Queries;

public abstract class Query<TResponse> : IQuery<TResponse>
{
    public DateTime Timestamp => DateTime.Now;
    public ValidationResult ValidationResult { get; set; } = new();

    public abstract bool IsValid();
}