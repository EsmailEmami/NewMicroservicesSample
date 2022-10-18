using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;
using MediatR;

namespace Application.PipeLines;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationFailures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => x.ErrorMessage).Distinct().ToArray();

        var errorMessage = string.Join(',', validationFailures);
        if (validationFailures.Any()) throw new ValidationException(errorMessage);

        return await next();
    }
}
