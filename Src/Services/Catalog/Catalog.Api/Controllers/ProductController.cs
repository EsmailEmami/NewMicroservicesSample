using Application.Core;
using Application.Core.Repositories;
using Application.Core.Repositories.Mongo;
using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            Product product = new Product();
            product.Set("name", "category", "summery", "description", "file", 12M);

            var data = _productService.AddProduct(product);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_productService.GetAllProducts());
        }

        [HttpGet("{productId:guid}")]
        public IActionResult Get([FromRoute] Guid productId)
        {
            return Ok(_productService.GetProductById(productId));
        }

        [HttpPut("{productId:guid}")]
        public IActionResult Update([FromRoute] Guid productId)
        {
            var product = _productService.GetProductById(productId);
            product.Set("updated", "category", "summery", "description", "file", 12M);
            _productService.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{productId:guid}")]
        public IActionResult Delete([FromRoute] Guid productId)
        {
            _productService.DeleteProduct(productId);
            return Ok();
        }
    }
}
