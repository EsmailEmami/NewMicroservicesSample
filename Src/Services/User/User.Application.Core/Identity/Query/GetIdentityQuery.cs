using User.Application.Core.Identity.QueryValidators;

namespace User.Application.Core.Identity.Query;
using Domain.Entities;

public class GetIdentityQuery : IdentityQuery<Identity>
{
    public GetIdentityQuery(long userId)
    {
        UserId = userId;
    }
    public override bool IsValid()
    {
        ValidationResult = new GetIdentityQueryValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}