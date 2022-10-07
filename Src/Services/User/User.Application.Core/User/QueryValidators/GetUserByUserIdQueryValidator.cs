using User.Application.Core.User.Queries;

namespace User.Application.Core.User.QueryValidators;

public class GetUserByUserIdQueryValidator : UserQueryValidator<GetUserByUserIdQuery, Domain.Entities.User>
{
    public GetUserByUserIdQueryValidator()
    {
        ValidateId();
    }
}