using Domain.Core.Queries;

namespace User.Application.Core.Identity.Query;

public abstract class IdentityQuery<TResponse> : Query<TResponse>
{
    public long UserId { get; protected set; }
}