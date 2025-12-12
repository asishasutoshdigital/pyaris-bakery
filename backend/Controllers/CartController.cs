using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using backend.Models;
using backend.Utils;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CartController> _logger;

        public CartController(IConfiguration configuration, ILogger<CartController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<CartResponse>> GetCart()
        {
            try
            {
                var transactionId = HttpContext.Session.GetString("xtran");
                if (string.IsNullOrEmpty(transactionId))
                {
                    transactionId = SessionHelper.GenerateTransactionId();
                    HttpContext.Session.SetString("xtran", transactionId);
                }

                var cartItems = new List<CartItem>();
                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var query = @"SELECT [id], [Transaction], [Menu Name], [Price], [Quantity], [Total Sale Amount], 
                                 [Flavour], [Weight], [Message On Cake], [Delivery Date] 
                                 FROM [Xsales slave] 
                                 WHERE [Transaction] = @Transaction";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Transaction", transactionId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                cartItems.Add(new CartItem
                                {
                                    Id = reader.GetInt32(0),
                                    Transaction = reader.GetString(1),
                                    MenuName = reader.GetString(2),
                                    Price = reader.GetDecimal(3),
                                    Quantity = reader.GetInt32(4),
                                    TotalSaleAmount = reader.GetDecimal(5),
                                    Flavour = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    Weight = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    MessageOnCake = reader.IsDBNull(8) ? null : reader.GetString(8),
                                    DeliveryDate = reader.IsDBNull(9) ? null : reader.GetString(9)
                                });
                            }
                        }
                    }
                }

                var subTotal = cartItems.Sum(x => x.TotalSaleAmount);
                var response = new CartResponse
                {
                    Items = cartItems,
                    SubTotal = subTotal,
                    Total = subTotal,
                    ItemCount = cartItems.Count
                };

                // Update session cart count
                HttpContext.Session.SetString("xcartqty", cartItems.Count.ToString());

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cart");
                return StatusCode(500, new { message = "Error fetching cart" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            try
            {
                var transactionId = HttpContext.Session.GetString("xtran");
                if (string.IsNullOrEmpty(transactionId))
                {
                    transactionId = SessionHelper.GenerateTransactionId();
                    HttpContext.Session.SetString("xtran", transactionId);
                }

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var insertQuery = @"INSERT INTO [Xsales slave] 
                                      ([Transaction], [Menu Name], [Price], [Quantity], [Total Sale Amount], [Flavour], 
                                       [Weight], [Message On Cake], [Delivery Date]) 
                                      VALUES (@Transaction, @MenuName, @Price, @Quantity, @TotalSaleAmount, @Flavour, 
                                              @Weight, @MessageOnCake, @DeliveryDate);
                                      SELECT CAST(SCOPE_IDENTITY() as int)";

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Transaction", transactionId);
                        command.Parameters.AddWithValue("@MenuName", request.MenuName);
                        command.Parameters.AddWithValue("@Price", request.Price);
                        command.Parameters.AddWithValue("@Quantity", request.Quantity);
                        command.Parameters.AddWithValue("@TotalSaleAmount", request.Price * request.Quantity);
                        command.Parameters.AddWithValue("@Flavour", (object?)request.Flavour ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Weight", (object?)request.Weight ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MessageOnCake", (object?)request.MessageOnCake ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DeliveryDate", (object?)request.DeliveryDate ?? DBNull.Value);

                        var id = (int)await command.ExecuteScalarAsync();

                        // Update cart count
                        var countQuery = "SELECT COUNT(*) FROM [Xsales slave] WHERE [Transaction] = @Transaction";
                        using (var countCommand = new SqlCommand(countQuery, connection))
                        {
                            countCommand.Parameters.AddWithValue("@Transaction", transactionId);
                            var count = (int)await countCommand.ExecuteScalarAsync();
                            HttpContext.Session.SetString("xcartqty", count.ToString());
                        }

                        return Ok(new { 
                            success = true, 
                            id = id,
                            message = "Item added to cart" 
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding to cart");
                return StatusCode(500, new { success = false, message = "Error adding to cart" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            try
            {
                var transactionId = HttpContext.Session.GetString("xtran");
                if (string.IsNullOrEmpty(transactionId))
                {
                    return BadRequest(new { success = false, message = "No active cart session" });
                }

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var deleteQuery = "DELETE FROM [Xsales slave] WHERE [id] = @Id AND [Transaction] = @Transaction";
                    using (var command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Transaction", transactionId);
                        
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            // Update cart count
                            var countQuery = "SELECT COUNT(*) FROM [Xsales slave] WHERE [Transaction] = @Transaction";
                            using (var countCommand = new SqlCommand(countQuery, connection))
                            {
                                countCommand.Parameters.AddWithValue("@Transaction", transactionId);
                                var count = (int)await countCommand.ExecuteScalarAsync();
                                HttpContext.Session.SetString("xcartqty", count.ToString());
                            }

                            return Ok(new { success = true, message = "Item removed from cart" });
                        }

                        return NotFound(new { success = false, message = "Item not found in cart" });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing from cart");
                return StatusCode(500, new { success = false, message = "Error removing from cart" });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> ClearCart()
        {
            try
            {
                var transactionId = HttpContext.Session.GetString("xtran");
                if (string.IsNullOrEmpty(transactionId))
                {
                    return Ok(new { success = true, message = "Cart is already empty" });
                }

                var connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    var deleteQuery = "DELETE FROM [Xsales slave] WHERE [Transaction] = @Transaction";
                    using (var command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Transaction", transactionId);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                HttpContext.Session.SetString("xcartqty", "0");
                return Ok(new { success = true, message = "Cart cleared" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing cart");
                return StatusCode(500, new { success = false, message = "Error clearing cart" });
            }
        }
    }
}
