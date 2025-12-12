using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IConfiguration configuration, ILogger<ProductsController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts(
            [FromQuery] string? subgroup = null,
            [FromQuery] string? flavour = null,
            [FromQuery] bool isFlavour = false)
        {
            try
            {
                var products = new List<ProductResponse>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query;
                    if (isFlavour && !string.IsNullOrEmpty(flavour))
                    {
                        query = @"SELECT DISTINCT [ID], [Menu Name], [Sell Price], [Group], [Sub Group], [Active], [Flavour] 
                                 FROM [XMaster Menu] 
                                 WHERE [Flavour] LIKE @Flavour AND [Active] = 1";
                    }
                    else if (!string.IsNullOrEmpty(subgroup))
                    {
                        query = @"SELECT DISTINCT [ID], [Menu Name], [Sell Price], [Group], [Sub Group], [Active], [Flavour] 
                                 FROM [XMaster Menu] 
                                 WHERE [Sub Group] = @SubGroup AND [Active] = 1";
                    }
                    else
                    {
                        query = @"SELECT DISTINCT [ID], [Menu Name], [Sell Price], [Group], [Sub Group], [Active], [Flavour] 
                                 FROM [XMaster Menu] 
                                 WHERE [Active] = 1";
                    }

                    using (var command = new SqlCommand(query, connection))
                    {
                        if (isFlavour && !string.IsNullOrEmpty(flavour))
                        {
                            command.Parameters.AddWithValue("@Flavour", $"%{flavour}%");
                        }
                        else if (!string.IsNullOrEmpty(subgroup))
                        {
                            command.Parameters.AddWithValue("@SubGroup", subgroup);
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                products.Add(new ProductResponse
                                {
                                    Id = reader.GetInt32(0),
                                    MenuName = reader.GetString(1),
                                    SellPrice = reader.GetDecimal(2),
                                    Group = reader.GetString(3),
                                    SubGroup = reader.GetString(4),
                                    Active = reader.GetBoolean(5),
                                    Flavour = reader.IsDBNull(6) ? null : reader.GetString(6)
                                });
                            }
                        }
                    }
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products");
                return StatusCode(500, new { message = "Error fetching products" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"SELECT [ID], [Menu Name], [Sell Price], [Group], [Sub Group], [Active], [Flavour], 
                                 [Feature 1], [Feature 2] 
                                 FROM [XMaster Menu] 
                                 WHERE [ID] = @Id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var product = new Product
                                {
                                    Id = reader.GetInt32(0),
                                    MenuName = reader.GetString(1),
                                    SellPrice = reader.GetDecimal(2),
                                    Group = reader.GetString(3),
                                    SubGroup = reader.GetString(4),
                                    Active = reader.GetBoolean(5),
                                    Flavour = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    Feature1 = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    Feature2 = reader.IsDBNull(8) ? null : reader.GetString(8)
                                };

                                return Ok(product);
                            }
                        }
                    }
                }

                return NotFound(new { message = "Product not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching product {ProductId}", id);
                return StatusCode(500, new { message = "Error fetching product" });
            }
        }

        [HttpGet("homepage-slider")]
        public async Task<ActionResult<IEnumerable<object>>> GetHomepageSlider()
        {
            try
            {
                var sliders = new List<object>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"SELECT [data], [options] 
                                 FROM mastercodes 
                                 WHERE location = 'websiteSlider' AND [Enabled] = 1 
                                 ORDER BY id";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                sliders.Add(new
                                {
                                    data = reader.GetString(0),
                                    imageUrl = reader.GetString(1)
                                });
                            }
                        }
                    }
                }

                return Ok(sliders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching homepage slider");
                return StatusCode(500, new { message = "Error fetching homepage slider" });
            }
        }
    }
}
