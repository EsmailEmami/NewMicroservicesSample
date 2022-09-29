using Domain.Core.Entities;
using Domain.Core.Events.EfCore;
using Domain.Core.Events.Mongo;

namespace Catalog.Domain.Entities;

public sealed class User : MongoEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }

    public void Set(string firstName, string lastName, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
    }
}