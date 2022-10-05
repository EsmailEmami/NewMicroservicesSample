using FluentValidation.Results;
using User.Application.Core.User.Validators;

namespace User.Application.Core.User.Commands;

public class CreateUserCommand : UserCommand<long>
{
    public CreateUserCommand(string firstName, string lastName, string userName, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Password = password;
    }

    public override bool IsValid()
    {
        ValidationResult = new CreateUserCommandValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}