using Catalog.Domain.Entities;
using MongoDB.Bson;

namespace Catalog.Application.Services;

public interface IProductService
{
    Guid AddProduct(Product product);
    List<Product> GetAllProducts();
    Product GetProductById(Guid productId); 
    void DeleteProduct(Guid productId);
    void UpdateProduct(Product product);
}