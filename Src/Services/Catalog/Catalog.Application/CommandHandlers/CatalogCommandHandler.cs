using Application.Core;
using Application.Core.Repositories.EfCore;
using Application.Core.Repositories.Mongo;
using Application.Events;
using Application.MessageBrokers;
using Catalog.Application.Commands.Product;
using Catalog.Application.Events;
using Catalog.Domain.Entities;
using Domain.Commands;
using Domain.Core.Commands;
using MediatR;

namespace Catalog.Application.CommandHandlers;

public class CatalogCommandHandler : ICommandHandler<AddProductCommand, Guid>
{
    #region ctor

    private readonly IEventBus _eventBus;
    private readonly IEfCoreRepository<Product, Guid> _efProductRepository;
    private readonly IMongoRepository<Product, Guid> _mongoProductRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CatalogCommandHandler(IEventBus eventBus, IEfCoreRepository<Product, Guid> efProductRepository, IMongoRepository<Product, Guid> mongoProductRepository, IUnitOfWork unitOfWork)
    {
        _eventBus = eventBus;
        _efProductRepository = efProductRepository;
        _mongoProductRepository = mongoProductRepository;
        _unitOfWork = unitOfWork;
    }

    #endregion

    public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var data = request.Product;
        var id = await _efProductRepository.InsertAndGetIdAsync(data);
        _unitOfWork.SaveAllChanges();

        await _mongoProductRepository.InsertAndGetIdAsync(data);

        await _eventBus.Commit(new ProductAdded(data));

        return id;
    }
}