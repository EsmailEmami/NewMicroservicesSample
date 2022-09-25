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


        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
