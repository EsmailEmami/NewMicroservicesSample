using Application.Core.Repositories.Mongo;
using Catalog.Application.Services;
using Catalog.Domain.Entities;
using MongoDB.Bson;

namespace Catalog.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IMongoRepository<Product, ObjectId> _productRepository;

    public ProductService(IMongoRepository<Product, ObjectId> productRepository)
    {
        _productRepository = productRepository;
    }
}