namespace PyarisAPI.Config
{
    public class AppSettings
    {
        public string PaymentGatewayURL { get; set; } = string.Empty;
        public string PaymentGateway { get; set; } = "PhonePe";
        public int MinimumOrderAmt { get; set; } = 400;
        public bool IsPaybackAvailable { get; set; } = true;
        public string UnavailableOutlets { get; set; } = string.Empty;
        public string UnavailablePincodes { get; set; } = string.Empty;
        public string SelectedGroup { get; set; } = string.Empty;
        public string SelectedSubGroup { get; set; } = string.Empty;
        public string AdminPhoneNumber { get; set; } = string.Empty;
        public string AdminSecondaryPhoneNumber { get; set; } = string.Empty;
        public string CustomerCareEmail { get; set; } = string.Empty;
        public string ImagePath { get; set; } = "images/productimages/";
        public int DisplayImageMax { get; set; } = 3;
    }
}
