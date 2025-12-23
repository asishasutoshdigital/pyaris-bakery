using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using PyarisAPI.Config;
using PyarisAPI.Services;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<PaymentController> _logger;
        private readonly PhonePeConfig _phonePeConfig;
        private readonly PaytmConfig _paytmConfig;

        public PaymentController(IConfiguration configuration, ILogger<PaymentController> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
            _logger = logger;
            _phonePeConfig = configuration.GetSection("PhonePe").Get<PhonePeConfig>() ?? new PhonePeConfig();
            _paytmConfig = configuration.GetSection("Paytm").Get<PaytmConfig>() ?? new PaytmConfig();
        }

        [HttpPost("phonepe/initiate")]
        public ActionResult<object> InitiatePhonePePayment([FromBody] PaymentRequest request)
        {
            try
            {
                // TODO: Implement PhonePe payment initiation
                var paymentData = new
                {
                    merchantId = _phonePeConfig.MerchantId,
                    transactionId = GenerateTransactionId(),
                    amount = request.Amount,
                    callbackUrl = _phonePeConfig.CallbackUrl
                };

                return Ok(new { success = true, paymentData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating PhonePe payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("phonepe/verify")]
        public ActionResult<object> VerifyPhonePePayment([FromBody] VerifyPaymentRequest request)
        {
            try
            {
                // TODO: Implement PhonePe payment verification
                bool isValid = true; // Placeholder
                
                if (isValid)
                {
                    UpdateOrderPaymentStatus(request.OrderNo, "PAID");
                    return Ok(new { success = true, message = "Payment verified successfully" });
                }
                
                return BadRequest(new { success = false, message = "Payment verification failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying PhonePe payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("paytm/initiate")]
        public ActionResult<object> InitiatePaytmPayment([FromBody] PaymentRequest request)
        {
            try
            {
                // TODO: Implement Paytm payment initiation
                var paymentData = new
                {
                    merchantId = _paytmConfig.MID,
                    orderId = request.OrderNo,
                    amount = request.Amount,
                    website = _paytmConfig.WEBSITE
                };

                return Ok(new { success = true, paymentData });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initiating Paytm payment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("paytm/verify")]
        public ActionResult<object> VerifyPaytmPayment([FromBody] VerifyPaymentRequest request)
        {
            try
            {
                // TODO: Implement Paytm payment verification
                bool isValid = true; // Placeholder
                
                if (isValid)
                {
                    UpdateOrderPaymentStatus(request.OrderNo, "PAID");
                    return Ok(new { success = true, message = "Payment verified successfully" });
                }
                
                return BadRequest(new { success = false, message = "Payment verification failed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying Paytm payment");
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateTransactionId()
        {
            return "TXN" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }

        private void UpdateOrderPaymentStatus(string orderNo, string status)
        {
            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Open();
                var cmd = new SqlCommand(
                    $"UPDATE [XSales Master] SET [Payment Status]='{status}', [Status]='CONFIRMED' WHERE [Order No]='{orderNo.Replace("'", "''")}'",
                    cn);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class PaymentRequest
    {
        public string OrderNo { get; set; } = "";
        public decimal Amount { get; set; }
        public string UserId { get; set; } = "";
    }

    public class VerifyPaymentRequest
    {
        public string OrderNo { get; set; } = "";
        public string TransactionId { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
