using FluentValidation;
using User.Application.Core.Identity.Query;

namespace User.Application.Core.Identity.QueryValidators;

public class IdentityQueryValidator<TQuery, TResponse> : AbstractValidator<TQuery> where TQuery : IdentityQuery<TResponse>
{
    public void ValidateUserId()
    {
        RuleFor(x => x.UserId)
            .NotNull().NotEqual(0)
            .WithMessage("لطفا شناسه را وارد کنید.");
    }
}