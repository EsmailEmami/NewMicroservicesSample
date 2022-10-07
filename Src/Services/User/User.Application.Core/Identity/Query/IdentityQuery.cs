using Domain.Core.Queries;

namespace User.Application.Core.Identity.Query;

public abstract class IdentityQuery<TResponse> : Query<TResponse>
{
    public Guid Id { get; protected set; }
}