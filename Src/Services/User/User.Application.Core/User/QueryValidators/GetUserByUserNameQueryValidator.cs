using User.Application.Core.User.Queries;

namespace User.Application.Core.User.QueryValidators;

public class GetUserByUserNameQueryValidator : UserQueryValidator<GetUserByUserNameQuery,Domain.Entities.User>
{
    public GetUserByUserNameQueryValidator()
    {
        ValidateUserName();
    }
}