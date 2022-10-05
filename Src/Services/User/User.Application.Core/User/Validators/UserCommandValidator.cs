using FluentValidation;
using User.Application.Core.User.Commands;

namespace User.Application.Core.User.Validators;

public class UserCommandValidator<TCommand, TResponse> : AbstractValidator<TCommand> where TCommand : UserCommand<TResponse>
{
    public void ValidateFirstName()
    {
        RuleFor(x => x.FirstName).NotEmpty()
            .WithMessage("لطفا نام را وارد کنید")
            .MaximumLength(150)
            .WithMessage("حداکثر کاراکتر برای نام 150 کاراکتر میباشد.");
    }

    public void ValidateLastName()
    {
        RuleFor(x => x.LastName).NotEmpty()
            .WithMessage("لطفا نام خانوادگی را وارد کنید")
            .MaximumLength(150)
            .WithMessage("حداکثر کاراکتر برای نام خانوادگی 150 کاراکتر میباشد.");
    }

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