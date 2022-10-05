using Domain.Core.Events.EfCore;
using Domain.Exceptions;

namespace User.Domain.Entities;

public class Identity : EfCoreEntity<Guid>
{
    public Identity(string password)
    {
        Password = password;
    }

    public override Guid Id { get; set; } = Guid.NewGuid();
    public string Password { get; private set; }
    public virtual User User { get; }

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

    public void SetPassword(string password) => Password = password;
    
}