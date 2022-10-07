using User.Application.Core.User.QueryValidators;

namespace User.Application.Core.User.Queries;

public class GetUserByUserNameQuery : UserQuery<Domain.Entities.User>
{
    public GetUserByUserNameQuery(string userName)
    {
        UserName = userName;
    }
    public override bool IsValid()
    {
        ValidationResult = new GetUserByUserNameQueryValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}