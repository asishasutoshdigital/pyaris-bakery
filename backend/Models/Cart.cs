namespace backend.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string Transaction { get; set; } = string.Empty;
        public string MenuName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public string? Flavour { get; set; }
        public string? Weight { get; set; }
        public string? MessageOnCake { get; set; }
        public string? DeliveryDate { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class AddToCartRequest
    {
        public string MenuName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Flavour { get; set; }
        public string? Weight { get; set; }
        public string? MessageOnCake { get; set; }
        public string? DeliveryDate { get; set; }
    }

    public class CartResponse
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
    }
}
