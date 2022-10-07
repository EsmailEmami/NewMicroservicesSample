using Domain.Core.Events.EfCore;

namespace User.Domain.Entities;

public class User : EfCoreEntity<long>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string UserName { get; }
    public Guid IdentityId { get; private set; }


    public virtual Identity Identiy { get; private set; }

    public User()
    {
    }

    public User(string firstName, string lastName, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
    }

    public User(string firstName, string lastName, string userName, string password) : this(firstName, lastName, userName)
    {
        SetIdentity(password);
    }

    public void SetIdentity(string password)
    {
        var identity = new Identity(password, this);
        SetIdentity(identity);
    }

    public void SetIdentity(Identity identity)
    {
        IdentityId = identity.Id;
        Identiy = identity;
    }
}