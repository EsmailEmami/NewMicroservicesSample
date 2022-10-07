using FluentValidation;
using User.Application.Core.Identity.Query;

namespace User.Application.Core.Identity.QueryValidators;

public class IdentityQueryValidator<TQuery, TResponse> : AbstractValidator<TQuery> where TQuery : IdentityQuery<TResponse>
{
    public void ValidateId()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEqual(Guid.Empty)
            .WithMessage("لطفا شناسه را وارد کنید.");
    }
}