using Domain.Core.Commands;

namespace User.Application.Core.User.Commands;

public abstract class UserCommand<TResponse> : Command<TResponse>
{
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string UserName { get; protected set; }
    public string Password { get; protected set; }
}