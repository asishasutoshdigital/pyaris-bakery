namespace PyarisAPI.Models
{
    public class PaymentDetails
    {
        public string TransactionId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }

    public class PhonePePayload
    {
        public string MerchantId { get; set; } = string.Empty;
        public string MerchantTransactionId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string MerchantUserId { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string RedirectMode { get; set; } = "POST";
        public string CallbackUrl { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
    }
}
