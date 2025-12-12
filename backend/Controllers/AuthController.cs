using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using backend.Models;
using backend.Utils;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"SELECT [Id], [MobileNo], [Name], [Email], [Address1], [Address2], [City], [Pincode], [Password] 
                                 FROM [xUser Details] 
                                 WHERE [MobileNo] = @MobileNo";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var storedPassword = reader.IsDBNull(8) ? "" : reader.GetString(8);
                                
                                // Verify password using hashing
                                if (SessionHelper.VerifyPassword(request.Password, storedPassword))
                                {
                                    var user = new UserResponse
                                    {
                                        Id = reader.GetInt32(0),
                                        MobileNo = reader.GetString(1),
                                        Name = reader.GetString(2),
                                        Email = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        Address1 = reader.IsDBNull(4) ? null : reader.GetString(4),
                                        Address2 = reader.IsDBNull(5) ? null : reader.GetString(5),
                                        City = reader.IsDBNull(6) ? null : reader.GetString(6),
                                        Pincode = reader.IsDBNull(7) ? null : reader.GetString(7)
                                    };

                                    // Store user info in session
                                    HttpContext.Session.SetString("xuser", user.MobileNo);
                                    HttpContext.Session.SetString("xusername", user.Name);

                                    return Ok(new { 
                                        success = true, 
                                        user = user,
                                        message = "Login successful" 
                                    });
                                }
                            }
                        }
                    }
                }

                return Unauthorized(new { success = false, message = "Invalid mobile number or password" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, new { success = false, message = "Login failed" });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Check if user already exists
                    var checkQuery = "SELECT COUNT(*) FROM [xUser Details] WHERE [MobileNo] = @MobileNo";
                    using (var checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        var count = (int)await checkCommand.ExecuteScalarAsync();
                        
                        if (count > 0)
                        {
                            return BadRequest(new { success = false, message = "Mobile number already registered" });
                        }
                    }

                    // Hash password
                    var hashedPassword = SessionHelper.HashPassword(request.Password);

                    // Insert new user
                    var insertQuery = @"INSERT INTO [xUser Details] 
                                      ([MobileNo], [Name], [Email], [Address1], [Address2], [City], [Pincode], [Password], [CreatedDate]) 
                                      VALUES (@MobileNo, @Name, @Email, @Address1, @Address2, @City, @Pincode, @Password, @CreatedDate);
                                      SELECT CAST(SCOPE_IDENTITY() as int)";

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MobileNo", request.MobileNo);
                        command.Parameters.AddWithValue("@Name", request.Name);
                        command.Parameters.AddWithValue("@Email", (object?)request.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address1", (object?)request.Address1 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Address2", (object?)request.Address2 ?? DBNull.Value);
                        command.Parameters.AddWithValue("@City", (object?)request.City ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Pincode", (object?)request.Pincode ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                        var userId = (int)await command.ExecuteScalarAsync();

                        var user = new UserResponse
                        {
                            Id = userId,
                            MobileNo = request.MobileNo,
                            Name = request.Name,
                            Email = request.Email,
                            Address1 = request.Address1,
                            Address2 = request.Address2,
                            City = request.City,
                            Pincode = request.Pincode
                        };

                        // Store user info in session
                        HttpContext.Session.SetString("xuser", user.MobileNo);
                        HttpContext.Session.SetString("xusername", user.Name);

                        return Ok(new { 
                            success = true, 
                            user = user,
                            message = "Registration successful" 
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, new { success = false, message = "Registration failed" });
            }
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return Ok(new { success = true, message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return StatusCode(500, new { success = false, message = "Logout failed" });
            }
        }

        [HttpGet("session")]
        public ActionResult GetSession()
        {
            try
            {
                var mobile = HttpContext.Session.GetString("xuser");
                var name = HttpContext.Session.GetString("xusername");

                if (!string.IsNullOrEmpty(mobile) && !string.IsNullOrEmpty(name))
                {
                    return Ok(new
                    {
                        isAuthenticated = true,
                        user = new { mobileNo = mobile, name = name }
                    });
                }

                return Ok(new { isAuthenticated = false });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting session");
                return StatusCode(500, new { message = "Error getting session" });
            }
        }
    }
}
