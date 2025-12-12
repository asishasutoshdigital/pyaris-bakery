using Microsoft.AspNetCore.Mvc;
using PyarisAPI.Models;
using PyarisAPI.Services;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound();

                var items = await _orderService.GetOrderItemsAsync(id);
                return Ok(new { order, items });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetCustomerOrders(string customerId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByCustomerAsync(customerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching customer orders: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = new Order
                {
                    CustomerId = request.CustomerId,
                    OrderDate = DateTime.Now,
                    TotalAmount = request.TotalAmount,
                    Status = "Pending",
                    DeliveryAddress = request.DeliveryAddress,
                    PhoneNumber = request.PhoneNumber,
                    PaymentMethod = request.PaymentMethod,
                    PaymentStatus = "Pending"
                };

                var orderId = await _orderService.CreateOrderAsync(order, request.Items);
                return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, new { orderId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating order: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            try
            {
                await _orderService.UpdateOrderStatusAsync(id, request.Status);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating order status: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class CreateOrderRequest
    {
        public string? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }

    public class UpdateStatusRequest
    {
        public string? Status { get; set; }
    }
}
