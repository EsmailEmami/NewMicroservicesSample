using Domain.Core.Events.EfCore;

namespace User.Domain.Entities;

public class Role : EfCoreEntity<Guid>
{
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}