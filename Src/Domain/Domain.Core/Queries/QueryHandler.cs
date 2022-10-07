using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Queries;

public abstract class QueryHandler
{
    public void CheckValidation<TQuery>(Query<TQuery> query)
    {
        if (!query.IsValid())
            throw new ValidationException(string.Join(", ", query.ValidationResult.Errors));
    }
}