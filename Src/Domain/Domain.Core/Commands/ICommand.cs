using FluentValidation.Results;
using MediatR;

namespace Domain.Core.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    DateTime Timestamp { get; } 
    ValidationResult ValidationResult { get; set;  }
    bool IsValid();
}

public interface ICommand : IRequest
{
    DateTime Timestamp { get; }
    ValidationResult ValidationResult { get; }
    bool IsValid();
}