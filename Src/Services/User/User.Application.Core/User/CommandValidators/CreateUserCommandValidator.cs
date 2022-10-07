using User.Application.Core.User.Commands;

namespace User.Application.Core.User.CommandValidators;

public class CreateUserCommandValidator : UserCommandValidator<CreateUserCommand, long>
{
    public CreateUserCommandValidator()
    {
        ValidateFirstName();
        ValidateLastName();
        ValidateUserName();
        ValidatePassword();
    }
}