using User.Application.Core.Identity.Query;

namespace User.Application.Core.Identity.QueryValidators;

public class GetIdentityQueryValidator : IdentityQueryValidator<GetIdentityQuery, Domain.Entities.Identity>
{
    public GetIdentityQueryValidator()
    {
        ValidateUserId();
    }
}