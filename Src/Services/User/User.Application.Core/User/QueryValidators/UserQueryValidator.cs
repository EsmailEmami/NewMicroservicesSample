using FluentValidation;
using User.Application.Core.User.Queries;

namespace User.Application.Core.User.QueryValidators;

public class UserQueryValidator<TQuery, TResponse> : AbstractValidator<TQuery> where TQuery : UserQuery<TResponse>
{
    public void ValidateUserName()
    {
        RuleFor(x => x.UserName).NotEmpty()
            .WithMessage("لطفا نام کاربری را وارد کنید")
            .MaximumLength(150)
            .WithMessage("حداکثر کاراکتر برای نام کاربری 150 کاراکتر میباشد.");
    }

    public void ValidatePassword()
    {
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("لطفا رمز عبور را وارد کنید")
            .MaximumLength(450)
            .WithMessage("حداکثر کاراکتر برای رمز عبور 450 کاراکتر میباشد.");
    }
}