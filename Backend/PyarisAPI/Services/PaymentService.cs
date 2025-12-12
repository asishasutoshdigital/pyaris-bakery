using RestSharp;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace PyarisAPI.Services
{
    public interface IPaymentService
    {
        Task<string> InitiatePhonePePaymentAsync(string orderId, decimal amount, string customerId);
        Task<bool> VerifyPhonePePaymentAsync(string transactionId, string merchantTransactionId);
        Task<bool> RefundPhonePePaymentAsync(string merchantTransactionId, decimal amount);
    }

    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> InitiatePhonePePaymentAsync(string orderId, decimal amount, string customerId)
        {
            try
            {
                var config = _configuration.GetSection("PaymentGateway:PhonePe");
                var merchantId = config["MerchantId"];
                var saltKey = config["SaltKey"];
                var baseUrl = config["BaseUrl"];

                var merchantTransactionId = $"ORDER_{orderId}_{DateTime.Now.Ticks}";
                var callbackUrl = "http://localhost:5000/api/payment/verify";

                var payloadData = new
                {
                    merchantId = merchantId,
                    merchantTransactionId = merchantTransactionId,
                    merchantUserId = customerId,
                    amount = (long)(amount * 100), // Amount in paise
                    redirectUrl = callbackUrl,
                    redirectMode = "POST",
                    mobileNumber = "9876543210",
                    paymentInstrument = new
                    {
                        type = "NETBANKING",
                        netBankingType = "NETBANKING"
                    }
                };

                var jsonPayload = JsonConvert.SerializeObject(payloadData);
                var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload));

                var checksum = GenerateChecksum(base64Payload + "/pg/v1/pay" + saltKey);

                var client = new RestClient($"{baseUrl}/pg/v1/pay");
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-VERIFY", $"{checksum}##1");

                var body = new { request = base64Payload };
                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);

                _logger.LogInformation($"PhonePe Payment Initiation Response: {response.Content}");

                return merchantTransactionId;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error initiating PhonePe payment: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> VerifyPhonePePaymentAsync(string transactionId, string merchantTransactionId)
        {
            try
            {
                var config = _configuration.GetSection("PaymentGateway:PhonePe");
                var merchantId = config["MerchantId"];
                var saltKey = config["SaltKey"];
                var baseUrl = config["BaseUrl"];

                var statusChecksum = GenerateChecksum($"/pg/v1/status/{merchantId}/{merchantTransactionId}" + saltKey);

                var client = new RestClient($"{baseUrl}/pg/v1/status/{merchantId}/{merchantTransactionId}");
                var request = new RestRequest("", Method.Get);
                request.AddHeader("X-VERIFY", $"{statusChecksum}##1");

                var response = await client.ExecuteAsync(request);

                _logger.LogInformation($"PhonePe Verification Response: {response.Content}");

                return response.IsSuccessful;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error verifying PhonePe payment: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> RefundPhonePePaymentAsync(string merchantTransactionId, decimal amount)
        {
            try
            {
                var config = _configuration.GetSection("PaymentGateway:PhonePe");
                var merchantId = config["MerchantId"];
                var saltKey = config["SaltKey"];
                var baseUrl = config["BaseUrl"];

                var refundPayload = new
                {
                    merchantId = merchantId,
                    originalTransactionId = merchantTransactionId,
                    refundAmount = (long)(amount * 100),
                    merchantRefundId = $"REFUND_{merchantTransactionId}_{DateTime.Now.Ticks}"
                };

                var jsonPayload = JsonConvert.SerializeObject(refundPayload);
                var base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload));

                var checksum = GenerateChecksum(base64Payload + "/pg/v1/refund" + saltKey);

                var client = new RestClient($"{baseUrl}/pg/v1/refund");
                var request = new RestRequest("", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("X-VERIFY", $"{checksum}##1");

                var body = new { request = base64Payload };
                request.AddJsonBody(body);

                var response = await client.ExecuteAsync(request);

                _logger.LogInformation($"PhonePe Refund Response: {response.Content}");

                return response.IsSuccessful;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error refunding PhonePe payment: {ex.Message}");
                return false;
            }
        }

        private string GenerateChecksum(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
