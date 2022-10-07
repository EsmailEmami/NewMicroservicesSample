using Domain.Core.Queries;

namespace User.Application.Core.User.Queries;

public abstract class UserQuery<TResponse> : Query<TResponse>
{
    public long UserId { get; protected set; }
    public string UserName { get; protected set; }
    public string Password { get; protected set; }
}