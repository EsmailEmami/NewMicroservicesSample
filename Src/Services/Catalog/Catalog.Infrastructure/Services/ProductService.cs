using Application.Core.Repositories.Mongo;
using Catalog.Application.Commands.Product;
using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Domain.Commands;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ICommandBus _commandBus;
    private readonly IMongoRepository<Product, Guid> _mongoProductRepository;
    private readonly IDistributedCache _redisCache;

    public ProductService(ICommandBus commandBus, IMongoRepository<Product, Guid> mongoProductRepository, IDistributedCache redisCache)
    {
        _commandBus = commandBus;
        _mongoProductRepository = mongoProductRepository;
        _redisCache = redisCache;
    }

    public Guid AddProduct(Product product)
    {
        try
        {
            var command = new AddProductCommand(product);
            return _commandBus.Send(command).Result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public List<Product> GetAllProducts()
    {
        try
        {
            return _mongoProductRepository.GetAllList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Product GetProductById(Guid productId)
    {
        try
        {
            return _mongoProductRepository.FirstOrDefault(x => x.Id == productId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteProduct(Guid productId)
    {
        try
        {
            _mongoProductRepository.Delete(productId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateProduct(Product product)
    {
        try
        {
            _mongoProductRepository.Update(product);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}