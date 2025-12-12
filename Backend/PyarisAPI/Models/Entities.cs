using System;

namespace PyarisAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? MenuName { get; set; }
        public string? Barcode { get; set; }
        public string? Group { get; set; }
        public string? SubGroup { get; set; }
        public string? Feature1 { get; set; }
        public string? Feature2 { get; set; }
        public string? Feature3 { get; set; }
        public string? Feature4 { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal Tax { get; set; }
        public int Stock { get; set; }
        public string? DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal MinWeight { get; set; }
        public string? Flavour { get; set; }
        public bool Active { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Payback { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class PaymentTransaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public string? TransactionReference { get; set; }
        public string? PaymentGateway { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? ResponseData { get; set; }
    }

    public class Refund
    {
        public int RefundId { get; set; }
        public int TransactionId { get; set; }
        public decimal RefundAmount { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        public DateTime RefundDate { get; set; }
        public string? ResponseData { get; set; }
    }
}
