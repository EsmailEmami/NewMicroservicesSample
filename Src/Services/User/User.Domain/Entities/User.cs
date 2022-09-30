using Domain.Core.Events.EfCore;

namespace User.Domain.Entities;

public class User : EfCoreEntity<long>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public User()
    {
    }

    public User Set(string firstName,string lastName, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;

        return this;
    }
}