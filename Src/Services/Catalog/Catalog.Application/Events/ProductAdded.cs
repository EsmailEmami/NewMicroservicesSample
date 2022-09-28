using Catalog.Domain.Entities;
using Domain.Core.Events;

namespace Catalog.Application.Events;

public class ProductAdded : Event
{
    public Product Product { get; set; }

    public ProductAdded(Product product)
    {
        Product = product;
    }
}