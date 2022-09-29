using Application.Core.Repositories.Mongo;
using Application.Events;
using BuildingBlocks.CatalogService.Product;
using Catalog.Application.Commands.Product;
using Catalog.Domain.Entities;
using Domain.Core.Commands;

namespace Catalog.Application.CommandHandlers;

public class CatalogCommandHandler : ICommandHandler<AddProductCommand, Guid>
{
    #region ctor

    private readonly IEventBus _eventBus;
    private readonly IMongoRepository<Product, Guid> _mongoProductRepository;

    public CatalogCommandHandler(IEventBus eventBus, IMongoRepository<Product, Guid> mongoProductRepository)
    {
        _eventBus = eventBus;
        _mongoProductRepository = mongoProductRepository;
    }

    #endregion

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _mongoProductRepository.InsertAsync(request.Product);

        await _eventBus.Commit(new ProductAddedEvent(request.Product.Id.ToString()));

        return product.Id;
    }
}