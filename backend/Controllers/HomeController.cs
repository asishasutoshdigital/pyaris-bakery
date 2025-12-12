using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    [HttpGet("slider")]
    public async Task<IActionResult> GetSlider()
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT [data], [options] FROM mastercodes WHERE location = 'websiteSlider' AND [Enabled] = 1 ORDER BY id";
            using var command = new SqlCommand(query, connection);

            var sliderItems = new List<object>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                sliderItems.Add(new
                {
                    data = reader["data"].ToString(),
                    options = reader["options"].ToString()
                });
            }

            return Ok(sliderItems);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error loading slider", error = ex.Message });
        }
    }
}
