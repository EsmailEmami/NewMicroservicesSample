using User.Application.Core.User.QueryValidators;

namespace User.Application.Core.User.Queries;

public class GetUserByUserIdQuery : UserQuery<Domain.Entities.User>
{
    public GetUserByUserIdQuery(long userId)
    {
        UserId = userId;
    }
    public override bool IsValid()
    {
        ValidationResult = new GetUserByUserIdQueryValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}