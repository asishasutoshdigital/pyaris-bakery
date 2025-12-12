using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ProductController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT [id], [name], [category], [price], [description], [imageUrl], [isAvailable] 
                         FROM [Products] WHERE [isAvailable] = 1 ORDER BY [name]";
            using var command = new SqlCommand(query, connection);

            var products = new List<Product>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    ImageUrl = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    IsAvailable = reader.GetBoolean(6)
                });
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error loading products", error = ex.Message });
        }
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT [id], [name], [category], [price], [description], [imageUrl], [isAvailable] 
                         FROM [Products] WHERE [category] = @Category AND [isAvailable] = 1 ORDER BY [name]";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Category", category);

            var products = new List<Product>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    ImageUrl = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    IsAvailable = reader.GetBoolean(6)
                });
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error loading products", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = @"SELECT [id], [name], [category], [price], [description], [imageUrl], [isAvailable] 
                         FROM [Products] WHERE [id] = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            
            if (await reader.ReadAsync())
            {
                var product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Price = reader.GetDecimal(3),
                    Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    ImageUrl = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    IsAvailable = reader.GetBoolean(6)
                };

                return Ok(product);
            }

            return NotFound(new { message = "Product not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error loading product", error = ex.Message });
        }
    }
}
