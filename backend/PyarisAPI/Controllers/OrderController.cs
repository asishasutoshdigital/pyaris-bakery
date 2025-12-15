using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using PyarisAPI.Services;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<OrderController> _logger;
        private readonly EmailService _emailService;
        private readonly SmsService _smsService;
        private readonly NotificationService _notificationService;

        public OrderController(IConfiguration configuration, ILogger<OrderController> logger, 
            EmailService emailService, SmsService smsService, NotificationService notificationService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
            _emailService = emailService;
            _smsService = smsService;
            _notificationService = notificationService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<object>> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                string orderNo = GenerateOrderNumber();
                
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    
                    // Insert order master
                    var cmd = new SqlCommand(
                        $"INSERT INTO [XSales Master] ([Order No],[Date],[Time],[User],[Name],[Email],[Phone],[Address1],[Address2],[City],[Pincode],[Total Amount],[Payment Mode],[Status]) " +
                        $"VALUES ('{orderNo}','{DateTime.Now.ToString("dd-MM-yyyy")}','{DateTime.Now.ToString("HH:mm:ss")}','{request.UserId.Replace("'", "''")}','{request.Name.Replace("'", "''")}','{request.Email.Replace("'", "''")}','{request.Phone.Replace("'", "''")}','{request.Address1.Replace("'", "''")}','{request.Address2.Replace("'", "''")}','{request.City.Replace("'", "''")}','{request.Pincode.Replace("'", "''")}','{request.TotalAmount}','{request.PaymentMode.Replace("'", "''")}','PENDING')",
                        cn);
                    cmd.ExecuteNonQuery();
                    
                    // Update cart items with order number
                    var updateCmd = new SqlCommand(
                        $"UPDATE [xSales Details] SET [Order No]='{orderNo}' WHERE [Xtran]='{request.SessionId.Replace("'", "''")}'",
                        cn);
                    updateCmd.ExecuteNonQuery();
                }
                
                // Send notifications
                await _emailService.SendEmailAsync(request.Email, "Order Confirmation", 
                    $"<h2>Order Confirmed!</h2><p>Your order {orderNo} has been placed successfully.</p>");
                await _smsService.SendSmsAsync(request.Phone, $"Your order {orderNo} has been placed successfully at Paris Bakery.", "");
                _notificationService.SaveNotification($"New order placed: {orderNo} by {request.Name}");
                
                return Ok(new { success = true, orderNo, message = "Order placed successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{orderNo}")]
        public ActionResult<object> GetOrder(string orderNo)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"SELECT * FROM [XSales Master] WHERE [Order No]='{orderNo.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    
                    if (dr.Read())
                    {
                        var order = new {
                            OrderNo = dr["Order No"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            User = dr["User"].ToString(),
                            Name = dr["Name"].ToString(),
                            Email = dr["Email"].ToString(),
                            Phone = dr["Phone"].ToString(),
                            TotalAmount = dr["Total Amount"].ToString(),
                            Status = dr["Status"].ToString()
                        };
                        return Ok(order);
                    }
                    
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customer/{userId}")]
        public ActionResult<object> GetCustomerOrders(string userId)
        {
            try
            {
                var orders = new List<object>();
                
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"SELECT * FROM [XSales Master] WHERE [User]='{userId.Replace("'", "''")}' ORDER BY [Date] DESC, [Time] DESC", cn);
                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        orders.Add(new {
                            OrderNo = dr["Order No"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            TotalAmount = dr["Total Amount"].ToString(),
                            Status = dr["Status"].ToString()
                        });
                    }
                }
                
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting customer orders");
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateOrderNumber()
        {
            return "ORD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }

    public class CreateOrderRequest
    {
        public string SessionId { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address1 { get; set; } = "";
        public string Address2 { get; set; } = "";
        public string City { get; set; } = "";
        public string Pincode { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public string PaymentMode { get; set; } = "";
    }
}
