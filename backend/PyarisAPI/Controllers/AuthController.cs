using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using PyarisAPI.Models;
using PyarisAPI.Services;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, 
            EmailService emailService, SmsService smsService)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
            _emailService = emailService;
            _smsService = smsService;
        }

        [HttpPost("check-user")]
        public ActionResult<object> CheckUser([FromBody] CheckUserRequest request)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"select * from [xUser Details] where [Mobile No]='{request.MobileNo.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    
                    if (dr.HasRows && dr.Read())
                    {
                        return Ok(new { exists = true, requiresPassword = true });
                    }
                    else
                    {
                        return Ok(new { exists = false, requiresPassword = false });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public ActionResult<object> Login([FromBody] LoginRequest request)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"select * from [xUser Details] where [Mobile No]='{request.MobileNo.Replace("'", "''")}' and [Password]='{request.Password.Replace("'", "''")}' COLLATE Latin1_General_CS_AS",
                        cn);
                    var dr = cmd.ExecuteReader();
                    
                    if (dr.HasRows && dr.Read())
                    {
                        var user = new CustomerModel
                        {
                            UserId = dr[0].ToString() ?? "",
                            MobileNumber = dr[1].ToString() ?? "",
                            Name = dr[2].ToString() ?? "",
                            Email = dr[3].ToString() ?? "",
                            Address1 = dr[4].ToString() ?? "",
                            Address2 = dr[5].ToString() ?? "",
                            City = dr[6].ToString() ?? "",
                            Pincode = dr[7].ToString() ?? ""
                        };
                        
                        // TODO: Generate JWT token
                        var token = "jwt_token_placeholder";
                        
                        return Ok(new { success = true, user, token });
                    }
                    else
                    {
                        return Unauthorized(new { success = false, message = "Invalid mobile number or password" });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<object>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    
                    // Check if user already exists
                    var checkCmd = new SqlCommand($"select COUNT(*) from [xUser Details] where [Mobile No]='{request.MobileNo.Replace("'", "''")}'", cn);
                    var count = (int)checkCmd.ExecuteScalar();
                    
                    if (count > 0)
                    {
                        return BadRequest(new { success = false, message = "Mobile number already registered" });
                    }
                    
                    // Insert new user
                    var cmd = new SqlCommand(
                        $"insert into [xUser Details] ([Mobile No],[Name],[Email],[Password],[Address1],[Address2],[City],[Pincode]) " +
                        $"values ('{request.MobileNo.Replace("'", "''")}','{request.Name.Replace("'", "''")}','{request.Email.Replace("'", "''")}','{request.Password.Replace("'", "''")}','{request.Address1.Replace("'", "''")}','{request.Address2.Replace("'", "''")}','{request.City.Replace("'", "''")}','{request.Pincode.Replace("'", "''")}')",
                        cn);
                    cmd.ExecuteNonQuery();
                    
                    // Send welcome email
                    await _emailService.SendEmailAsync(request.Email, "Welcome to Paris Bakery", 
                        $"<h1>Welcome {request.Name}!</h1><p>Thank you for registering with Paris Bakery.</p>");
                    
                    return Ok(new { success = true, message = "Registration successful" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<object>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"select [Password],[Email],[Name] from [xUser Details] where [Mobile No]='{request.MobileNo.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    
                    if (dr.HasRows && dr.Read())
                    {
                        var password = dr[0].ToString();
                        var email = dr[1].ToString();
                        var name = dr[2].ToString();
                        
                        if (!string.IsNullOrEmpty(email))
                        {
                            await _emailService.SendEmailAsync(email, "Password Recovery - Paris Bakery",
                                $"<h2>Hello {name}</h2><p>Your password is: <strong>{password}</strong></p>");
                        }
                        
                        return Ok(new { success = true, message = "Password sent to your email" });
                    }
                    else
                    {
                        return NotFound(new { success = false, message = "Mobile number not found" });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during password recovery");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class CheckUserRequest
    {
        public string MobileNo { get; set; } = "";
    }

    public class LoginRequest
    {
        public string MobileNo { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class RegisterRequest
    {
        public string MobileNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Address1 { get; set; } = "";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "";
        public string Pincode { get; set; } = "";
    }

    public class ForgotPasswordRequest
    {
        public string MobileNo { get; set; } = "";
    }
}
