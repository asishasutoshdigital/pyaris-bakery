using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using PyarisAPI.Models;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<CartController> _logger;

        public CartController(IConfiguration configuration, ILogger<CartController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
        }

        [HttpGet("{sessionId}")]
        public ActionResult<object> GetCart(string sessionId)
        {
            try
            {
                var cartItems = new List<object>();
                decimal totalAmount = 0;
                int totalQty = 0;

                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"SELECT * FROM [xSales Details] WHERE [Xtran]='{sessionId.Replace("'", "''")}'", cn);
                    var dr = cmd.ExecuteReader();
                    
                    while (dr.Read())
                    {
                        var item = new {
                            Id = dr["id"].ToString(),
                            ProductId = dr["menu id"].ToString(),
                            ProductName = dr["item"].ToString(),
                            Quantity = dr["qty"].ToString(),
                            Price = dr["price"].ToString(),
                            Amount = dr["amount"].ToString()
                        };
                        cartItems.Add(item);
                        totalAmount += decimal.Parse(item.Amount);
                        totalQty += int.Parse(item.Quantity);
                    }
                }

                return Ok(new { cartItems, totalAmount, totalQty });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("add")]
        public ActionResult AddToCart([FromBody] AddToCartRequest request)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"INSERT INTO [xSales Details] ([Xtran],[menu id],[item],[qty],[price],[amount]) " +
                        $"VALUES ('{request.SessionId.Replace("'", "''")}','{request.ProductId.Replace("'", "''")}','{request.ProductName.Replace("'", "''")}','{request.Quantity}','{request.Price}','{request.Amount}')",
                        cn);
                    cmd.ExecuteNonQuery();
                }

                return Ok(new { success = true, message = "Item added to cart" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding to cart");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update")]
        public ActionResult UpdateCart([FromBody] UpdateCartRequest request)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand(
                        $"UPDATE [xSales Details] SET [qty]='{request.Quantity}', [amount]='{request.Amount}' WHERE [id]='{request.ItemId}'",
                        cn);
                    cmd.ExecuteNonQuery();
                }

                return Ok(new { success = true, message = "Cart updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cart");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("remove/{itemId}")]
        public ActionResult RemoveFromCart(string itemId)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"DELETE FROM [xSales Details] WHERE [id]='{itemId.Replace("'", "''")}'", cn);
                    cmd.ExecuteNonQuery();
                }

                return Ok(new { success = true, message = "Item removed from cart" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing from cart");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("clear/{sessionId}")]
        public ActionResult ClearCart(string sessionId)
        {
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand($"DELETE FROM [xSales Details] WHERE [Xtran]='{sessionId.Replace("'", "''")}'", cn);
                    cmd.ExecuteNonQuery();
                }

                return Ok(new { success = true, message = "Cart cleared" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class AddToCartRequest
    {
        public string SessionId { get; set; } = "";
        public string ProductId { get; set; } = "";
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }

    public class UpdateCartRequest
    {
        public string ItemId { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }
}
