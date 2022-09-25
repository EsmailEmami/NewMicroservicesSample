using Domain.Attributes;
using Domain.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Domain.Entities;

[BsonCollection("Products")]
public class Product : Entity<Guid>
{
    public Product()
    {
        Id  = Guid.NewGuid();
    }

    public string Name { get; private set; }
    public string Category { get; private set; }
    public string Summary { get; private set; }
    public string Description { get; private set; }
    public string ImageFile { get; private set; }
    public decimal Price { get; private set; }

    public void Set(string name, string category, string summary, string description, string imageFile, decimal price)
    {
        Name = name;
        Category = category;
        Summary = summary;
        Description = description;
        ImageFile = imageFile;
        Price = price;
    }
}