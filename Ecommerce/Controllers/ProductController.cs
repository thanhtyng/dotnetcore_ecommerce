using Ecommerce.Services.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetAllProduct()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("/api/product/{id}")]
        public ActionResult GetProduct(int id)
        {
            _logger.LogInformation("Getting a product");
            var product = _productService.GetProductById(id);
            return Ok(product);
        }
    }
}