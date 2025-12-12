using Microsoft.AspNetCore.Mvc;
using PyarisAPI.Models;
using PyarisAPI.Services;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching products: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("group/{group}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByGroup(string group)
        {
            try
            {
                var products = await _productService.GetProductsByGroupAsync(group);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching products by group: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("subgroup/{subGroup}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsBySubGroup(string subGroup)
        {
            try
            {
                var products = await _productService.GetProductsBySubGroupAsync(subGroup);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching products by sub-group: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            try
            {
                product.ModifiedDate = DateTime.Now;
                await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            try
            {
                product.ModifiedDate = DateTime.Now;
                await _productService.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting product: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
