using System.Web;

namespace PyarisAPI.Services
{
    public class SmsService
    {
        private readonly IConfiguration _configuration;
        private readonly string _smsUrl;
        private readonly string _smsUserName;
        private readonly string _smsPassword;
        private readonly string _smsSenderId;
        private readonly string _smsChannel;
        private readonly string _smsDsc;
        private readonly string _smsFlash;
        private readonly string _smsRoute;
        private readonly string _smsPeid;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smsUrl = configuration["SMS:Url"] ?? "";
            _smsUserName = configuration["SMS:UserName"] ?? "";
            _smsPassword = configuration["SMS:Password"] ?? "";
            _smsSenderId = configuration["SMS:SenderId"] ?? "";
            _smsChannel = configuration["SMS:Channel"] ?? "";
            _smsDsc = configuration["SMS:DSC"] ?? "";
            _smsFlash = configuration["SMS:FlashSMS"] ?? "";
            _smsRoute = configuration["SMS:Route"] ?? "";
            _smsPeid = configuration["SMS:PEID"] ?? "";
        }

        public async Task SendSmsAsync(string to, string message, string templateId = "")
        {
            try
            {
                var uriBuilder = new UriBuilder(_smsUrl);
                var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);

                queryParams.Add("user", _smsUserName);
                queryParams.Add("password", _smsPassword);
                queryParams.Add("senderid", _smsSenderId);
                queryParams.Add("channel", _smsChannel);
                queryParams.Add("DCS", _smsDsc);
                queryParams.Add("flashsms", _smsFlash);
                queryParams.Add("route", _smsRoute);
                queryParams.Add("PEID", _smsPeid);
                queryParams.Add("number", to);
                queryParams.Add("text", message);
                queryParams.Add("DLTTemplateID", templateId);

                uriBuilder.Query = queryParams.ToString();
                string url = uriBuilder.ToString();

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    LogService.Debug($"SMS sent successfully to {to}: {content}");
                }
            }
            catch (Exception ex)
            {
                LogService.Error($"Error sending SMS to {to}", ex);
            }
        }

        public async Task SendOrderNotificationAsync(string to, string orderNo, string templateId)
        {
            string message = $"Your order {orderNo} has been placed successfully at Paris Bakery. Thank you!";
            await SendSmsAsync(to, message, templateId);
        }
    }
}
