using BuildingBlocks.CatalogService.Product;
using DnsClient.Internal;
using Domain.Core.Events;
using Microsoft.Extensions.Logging;

namespace Basket.Application.EventHandlers;

public class BasketEventHandler : IEventHandler<ProductAddedEvent>
{
    private readonly ILogger<BasketEventHandler> _logger;

    public BasketEventHandler(ILogger<BasketEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductAddedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("basket api consumed");
        return Task.CompletedTask;
    }
}