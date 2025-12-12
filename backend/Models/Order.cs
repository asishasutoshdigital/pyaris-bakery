namespace backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerMobile { get; set; } = string.Empty;
        public string? CustomerEmail { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderDate { get; set; }
        public string? DeliveryDate { get; set; }
        public string? PaymentId { get; set; }
        public string PaymentStatus { get; set; } = "Pending";
        public string PaymentMethod { get; set; } = string.Empty;
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string? Flavour { get; set; }
        public string? Weight { get; set; }
        public string? MessageOnCake { get; set; }
    }

    public class CreateOrderRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string City { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string DeliveryDate { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "Online";
    }

    public class OrderResponse
    {
        public int OrderId { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string? PaymentUrl { get; set; }
    }
}
