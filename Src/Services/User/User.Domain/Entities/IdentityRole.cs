using Domain.Core.Entities;

namespace User.Domain.Entities
{
    public class IdentityRole : Entity
    {
        public IdentityRole()
        {
        }

        public IdentityRole(Guid identityId, Guid roleId)
        {
            IdentityId = identityId;
            RoleId = roleId;
        }

        public Guid IdentityId { get; }
        public Guid RoleId { get; }
        public virtual Identity Identity { get; set; }
        public virtual Role Role { get; set; }
    }
}
