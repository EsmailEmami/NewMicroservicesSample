using Domain.Core.Events.EfCore;
using Domain.Exceptions;

namespace User.Domain.Entities;

public class Identity : EfCoreEntity<Guid>
{
    public Identity()
    {
    }

    public Identity(string password, User user)
    {
        Password = password;
        User = user;
    }

    public override Guid Id { get; set; } = Guid.NewGuid();

    public string Password { get; private set; }
    public virtual User User { get; }

    public virtual ICollection<IdentityRole> IdentityRole { get; } = new List<IdentityRole>();

    #region IdentityRole

    public void AddIdentityRole(Guid roleId)
    {
        if (IdentityRole.Any(x => x.RoleId == roleId))
            throw new ApplicationException("مقدار مورد نظر تکراری است.");

        IdentityRole.Add(new IdentityRole(Id, roleId));
    }
    public void RemoveIdentityRole(Guid roleId)
    {
        var identityRole = IdentityRole.SingleOrDefault(x => x.RoleId == roleId);
        if (identityRole == null) throw new EntityNotFoundException("مقدار مورد نظر یافت نشد.");
        IdentityRole.Remove(identityRole);
    }
    public void ClearIdentityRole() => IdentityRole.Clear();

    #endregion

    public void SetPassword(string password) => Password = password;

}