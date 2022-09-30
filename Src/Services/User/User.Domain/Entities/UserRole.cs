using Domain.Core.Entities;

namespace User.Domain.Entities
{
    public class UserRole : Entity
    {
        public long UserId { get; set; }
        public Guid RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
