using System.ComponentModel.DataAnnotations;
using Domain.Core.Events.EfCore;
using Domain.Exceptions;

namespace User.Domain.Entities;

public class Role : EfCoreEntity<Guid>
{
    public Role()
    {
        
    }

    public override Guid Id { get; set; } = Guid.NewGuid();

    public Role(RoleType type)
    {
        Type = type;
    }
    public RoleType Type { get; }
    public virtual ICollection<IdentityRole> IdentityRole { get; } = new List<IdentityRole>();

    #region IdentityRole

    public void AddIdentityRole(Guid identityId)
    {
        if (IdentityRole.Any(x => x.IdentityId == identityId && x.RoleId == Id))
            throw new ApplicationException("مقدار مورد نظر تکراری است.");

        IdentityRole.Add(new IdentityRole(identityId, Id));
    }
    public void RemoveIdentityRole(Guid identityId)
    {
        var identityRole = IdentityRole.SingleOrDefault(x => x.IdentityId == identityId);
        if (identityRole == null) throw new EntityNotFoundException("مقدار مورد نظر یافت نشد.");
        IdentityRole.Remove(identityRole);
    }
    public void ClearIdentityRole() => IdentityRole.Clear();

    #endregion
}

public enum RoleType
{
    [Display(Name = "سیستم ادمین")]
    SystemAdmin = 0,
}