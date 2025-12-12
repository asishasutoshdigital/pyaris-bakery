using PyarisAPI.Models;

namespace PyarisAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId);
        Task<int> CreateOrderAsync(Order order, List<OrderItem> items);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly PyarisAPI.Data.PyarisDbContext _context;

        public OrderService(PyarisAPI.Data.PyarisDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return _context.Orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).OrderByDescending(o => o.OrderDate).ToList();
        }

        public async Task<int> CreateOrderAsync(Order order, List<OrderItem> items)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                item.OrderId = order.OrderId;
                _context.OrderItems.Add(item);
            }

            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
        {
            return _context.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        }
    }
}
