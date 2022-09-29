using Domain.Core.Events;

namespace BuildingBlocks.CatalogService.Product;

public class ProductAddedEvent : Event
{
    public string TestValue { get; }

    public ProductAddedEvent(string testValue)
    {
        TestValue = testValue;
    }
}