using Domain.Core.Queries;

namespace User.Application.Core.User.Queries;

public abstract class UserQuery<TResponse> : Query<TResponse>
{
    public string UserName { get; protected set; }
    public string Password { get; protected set; }
}