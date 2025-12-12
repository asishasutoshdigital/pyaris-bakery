using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private readonly IWebHostEnvironment _environment;

    public CatalogController(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
        _environment = environment;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetProductsByCategory(
        [FromQuery] string? subgroup,
        [FromQuery] string? grp,
        [FromQuery] bool isFlavour = false)
    {
        try
        {
            // Initialize session if needed
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("xtran")))
            {
                HttpContext.Session.SetString("xtran", GenerateRandomPin(20));
                HttpContext.Session.SetString("xcartqty", "0");
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var products = new List<ProductItem>();
            SqlCommand cmdx;

            if (!string.IsNullOrEmpty(grp))
            {
                var query = "SELECT [id],[menu name],[sell price],[Group],[active] FROM [XMaster Menu] WHERE [Group]=@Group AND [Sub group] LIKE '%' + @SubGroup + '%' AND [active] = 1";
                cmdx = new SqlCommand(query, connection);
                cmdx.Parameters.AddWithValue("@Group", grp);
                cmdx.Parameters.AddWithValue("@SubGroup", subgroup?.Replace("'", "") ?? "");
            }
            else
            {
                var query = "SELECT [id],[menu name],[sell price],[Group],[active] FROM [XMaster Menu] WHERE [Sub group] LIKE '%' + @SubGroup + '%' AND [active] = 1";
                cmdx = new SqlCommand(query, connection);
                cmdx.Parameters.AddWithValue("@SubGroup", subgroup?.Replace("'", "") ?? "");
            }

            using var reader = await cmdx.ExecuteReaderAsync();
            var random = new Random();

            while (await reader.ReadAsync())
            {
                var group = reader["Group"].ToString() ?? "";
                if (group != "CAKES")
                {
                    var productId = reader["id"].ToString() ?? "";
                    var menuName = reader["menu name"].ToString() ?? "";
                    var sellPrice = reader["sell price"].ToString() ?? "";
                    var active = reader["active"].ToString() ?? "";

                    // Generate image path
                    var imagePath = GetProductImagePath(menuName);

                    // Generate random rating
                    var ratingInt = random.Next(4, 5);
                    var ratingDecimal = random.Next(0, 10);
                    var rating = double.Parse($"{ratingInt}.{ratingDecimal}");

                    products.Add(new ProductItem
                    {
                        Id = productId,
                        MenuName = menuName,
                        SellPrice = sellPrice,
                        Group = group,
                        Active = active,
                        ImagePath = imagePath,
                        Rating = rating
                    });
                }
            }

            return Ok(new
            {
                subgroup = subgroup,
                products = products,
                count = products.Count
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error loading products", error = ex.Message });
        }
    }

    private string GetProductImagePath(string menuName)
    {
        try
        {
            var imagePath = "/images/svg-icons/img-placeholder.svg";
            var imageFolder = Path.Combine(_environment.ContentRootPath, "..", "images", "products");
            
            if (Directory.Exists(imageFolder))
            {
                var files = Directory.GetFiles(imageFolder, $"{menuName}*.jpg");
                if (files.Length > 0)
                {
                    var fileName = Path.GetFileName(files[0]);
                    imagePath = $"/images/products/{fileName}";
                }
            }
            
            return imagePath;
        }
        catch
        {
            return "/images/svg-icons/img-placeholder.svg";
        }
    }

    private string GenerateRandomPin(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }
        return result.ToString();
    }
}

public class ProductItem
{
    public string Id { get; set; } = string.Empty;
    public string MenuName { get; set; } = string.Empty;
    public string SellPrice { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
    public string Active { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public double Rating { get; set; }
}
