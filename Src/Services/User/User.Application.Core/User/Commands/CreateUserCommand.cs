using Domain.Core.Commands;

namespace User.Application.Core.User.Commands;

public class CreateUserCommand : ICommand<long>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string UserName { get; }

    public CreateUserCommand(string firstName, string lastName, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
    }
}