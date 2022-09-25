using Domain.Attributes;
using Domain.Core.Entities;

namespace Application.Extensions;

public static class MongoExtensions
{
    public static string GetCollectionName(Type entity)
    {
        var attr = entity.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as BsonCollectionAttribute;
        if (attr == null)
            throw new InvalidOperationException(nameof(BsonCollectionAttribute));

        return attr.CollectionName;
    }
}