using BlazorProducts.Server.Repository;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _repo.GetProduct(id);
            return Ok(product);
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest();

            //model validation…
            await _repo.CreateProduct(product);
            return Created("", product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            //additional product and model validation checks

            var dbProduct = await _repo.GetProduct(id);
            if (dbProduct == null)
                return NotFound();

            await _repo.UpdateProduct(product, dbProduct);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _repo.GetProduct(id);
            if (product == null)
                return NotFound();

            await _repo.DeleteProduct(product);

            return NoContent();
        }
    }
}
