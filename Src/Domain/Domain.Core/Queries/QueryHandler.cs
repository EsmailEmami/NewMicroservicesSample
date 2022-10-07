using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Queries;

public abstract class QueryHandler<TRequest, TResponse> : IQueryHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    public void CheckValidation<TQuery>(Query<TQuery> query)
    {
        if (!query.IsValid())
            throw new ValidationException(string.Join(", ", query.ValidationResult.Errors));
    }
}