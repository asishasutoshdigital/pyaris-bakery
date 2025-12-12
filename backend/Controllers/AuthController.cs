using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? "";
    }

    [HttpPost("check-user")]
    public async Task<IActionResult> CheckUser([FromBody] string mobileNo)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM [xUser Details] WHERE [Mobile No] = @MobileNo";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MobileNo", mobileNo);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return Ok(new { exists = true });
            }

            return Ok(new { exists = false });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error checking user", error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.MobileNo) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Please enter mobile number and password" });
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            var query = "SELECT * FROM [xUser Details] WHERE [Mobile No] = @MobileNo AND [Password] = @Password COLLATE Latin1_General_CS_AS";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
            command.Parameters.AddWithValue("@Password", request.Password);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                var user = new
                {
                    mobileNo = reader["Mobile No"].ToString(),
                    name = reader["Name"].ToString(),
                    email = reader.GetOrdinal("Email") >= 0 ? reader["Email"].ToString() : ""
                };

                // Store user info in session
                HttpContext.Session.SetString("xuser", user.mobileNo ?? "");
                HttpContext.Session.SetString("xusername", user.name ?? "");

                return Ok(new { success = true, user });
            }

            return Unauthorized(new { message = "The mobile number and password combination provided is not valid" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Login failed", error = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.MobileNo) || string.IsNullOrEmpty(request.Name) || 
                string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Please fill all required fields" });
            }

            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            // Check if user already exists
            var checkQuery = "SELECT COUNT(*) FROM [xUser Details] WHERE [Mobile No] = @MobileNo";
            using var checkCommand = new SqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@MobileNo", request.MobileNo);

            var countResult = await checkCommand.ExecuteScalarAsync();
            var count = countResult != null ? Convert.ToInt32(countResult) : 0;
            if (count > 0)
            {
                return BadRequest(new { message = "User with this mobile number already exists" });
            }

            // Insert new user
            var insertQuery = @"INSERT INTO [xUser Details] ([Mobile No], [Name], [Password], [Email], [Address]) 
                               VALUES (@MobileNo, @Name, @Password, @Email, @Address)";
            using var insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@MobileNo", request.MobileNo);
            insertCommand.Parameters.AddWithValue("@Name", request.Name);
            insertCommand.Parameters.AddWithValue("@Password", request.Password);
            insertCommand.Parameters.AddWithValue("@Email", request.Email ?? "");
            insertCommand.Parameters.AddWithValue("@Address", request.Address ?? "");

            await insertCommand.ExecuteNonQueryAsync();

            // Auto-login after registration
            HttpContext.Session.SetString("xuser", request.MobileNo);
            HttpContext.Session.SetString("xusername", request.Name);

            return Ok(new { success = true, message = "Registration successful" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Registration failed", error = ex.Message });
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok(new { success = true, message = "Logged out successfully" });
    }

    [HttpGet("session")]
    public IActionResult GetSession()
    {
        var user = HttpContext.Session.GetString("xuser");
        var username = HttpContext.Session.GetString("xusername");

        if (string.IsNullOrEmpty(user))
        {
            return Ok(new { isAuthenticated = false });
        }

        return Ok(new { 
            isAuthenticated = true, 
            user = new { mobileNo = user, name = username }
        });
    }
}
