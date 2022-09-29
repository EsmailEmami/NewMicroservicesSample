using BuildingBlocks.CatalogService.Product;
using Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.EventHandlers;

public class CatalogEventHandler : IEventHandler<ProductAddedEvent>
{
    private readonly ILogger<CatalogEventHandler> _logger;

    public CatalogEventHandler(ILogger<CatalogEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("catalog consumed");
        return Task.CompletedTask;
    }
}