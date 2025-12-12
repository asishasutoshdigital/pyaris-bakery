namespace backend.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public decimal SellPrice { get; set; }
        public string Group { get; set; } = string.Empty;
        public string SubGroup { get; set; } = string.Empty;
        public string? Flavour { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
        public string? Feature1 { get; set; }
        public string? Feature2 { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public string MenuName { get; set; } = string.Empty;
        public decimal SellPrice { get; set; }
        public string Group { get; set; } = string.Empty;
        public string SubGroup { get; set; } = string.Empty;
        public string? Flavour { get; set; }
        public string? ImageUrl { get; set; }
        public bool Active { get; set; }
    }
}
