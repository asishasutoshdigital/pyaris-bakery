namespace PyarisAPI.Config
{
    public class PhonePeConfig
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
}
