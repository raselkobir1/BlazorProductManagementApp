using BlazorProducts.Server.Repository;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("get-products")]
        public async Task<IActionResult> Get()
        {
            var products = await _repo.GetProducts();
            return Ok(products);
        }
        [HttpGet("get-paginated-products")]
        public async Task<IActionResult> Get([FromQuery] ProductParameters productParameters)
        {
            var products = await _repo.GetProducts(productParameters);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(products.MetaData));

            return Ok(products);
        }
    }
}
