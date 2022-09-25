using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, IRequest<TResponse>
{
    private readonly IValidatorFactory _validationFactory;

    public ValidationBehavior(IValidatorFactory validationFactory)
    {
        _validationFactory = validationFactory;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validator = _validationFactory.GetValidator(request.GetType());
        var result = await validator?.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)!;

        if (result is { IsValid: false })
        {
            throw new ValidationException(result.Errors.ToString());
        }

        var response = await next();

        return response;
    }
}