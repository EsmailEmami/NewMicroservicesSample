using Catalog.Application.Events;
using Domain.Core.Events;

namespace Catalog.Application.EventHandlers;

public class CatalogEventHandlers : IEventHandler<ProductAdded>
{
    public Task Handle(ProductAdded notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}