namespace backend.Utils
{
    public class AppSettings
    {
        public string AdminPhoneNumber { get; set; } = string.Empty;
        public string AdminSecondaryPhoneNumber { get; set; } = string.Empty;
        public string CustomerCareEmail { get; set; } = string.Empty;
        public int MinimumOrderAmount { get; set; }
        public bool IsPaybackAvailable { get; set; }
        public string PaymentGateway { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public PhonePeSettings PhonePe { get; set; } = new();
        public CCavenueSettings CCAvenue { get; set; } = new();
        public SmsSettings SMS { get; set; } = new();
        public EmailSettings Email { get; set; } = new();
    }

    public class PhonePeSettings
    {
        public string MerchantId { get; set; } = string.Empty;
        public string SaltKey { get; set; } = string.Empty;
        public string SaltIndex { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string PayEndPoint { get; set; } = string.Empty;
        public string RefundEndPoint { get; set; } = string.Empty;
        public string StatusEndPoint { get; set; } = string.Empty;
        public string CallbackUrl { get; set; } = string.Empty;
    }

    public class CCavenueSettings
    {
        public string MerchantId { get; set; } = string.Empty;
        public string WorkingKey { get; set; } = string.Empty;
        public string AccessCode { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public string RedirectUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;
    }

    public class SmsSettings
    {
        public string Url { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
        public string DSC { get; set; } = string.Empty;
        public string FlashSMS { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public string PEID { get; set; } = string.Empty;
        public string TemplateNewOrder { get; set; } = string.Empty;
        public string TemplateOrderPlacing { get; set; } = string.Empty;
        public string TemplateWebOrderUpdate { get; set; } = string.Empty;
        public string TemplateOrderUpdate { get; set; } = string.Empty;
    }

    public class EmailSettings
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
    }
}
