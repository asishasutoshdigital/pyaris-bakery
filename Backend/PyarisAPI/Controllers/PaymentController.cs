using Microsoft.AspNetCore.Mvc;
using PyarisAPI.Services;

namespace PyarisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("phonepe/initiate")]
        public async Task<IActionResult> InitiatePhonePePayment([FromBody] PaymentInitiateRequest request)
        {
            try
            {
                var merchantTransactionId = await _paymentService.InitiatePhonePePaymentAsync(
                    request.OrderId, request.Amount, request.CustomerId);

                return Ok(new { success = true, merchantTransactionId });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error initiating PhonePe payment: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Payment initiation failed" });
            }
        }

        [HttpPost("phonepe/verify")]
        public async Task<IActionResult> VerifyPhonePePayment([FromBody] PaymentVerifyRequest request)
        {
            try
            {
                var isValid = await _paymentService.VerifyPhonePePaymentAsync(
                    request.TransactionId, request.MerchantTransactionId);

                return Ok(new { success = isValid });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error verifying PhonePe payment: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Payment verification failed" });
            }
        }

        [HttpPost("phonepe/refund")]
        public async Task<IActionResult> RefundPhonePePayment([FromBody] PaymentRefundRequest request)
        {
            try
            {
                var isRefunded = await _paymentService.RefundPhonePePaymentAsync(
                    request.MerchantTransactionId, request.Amount);

                return Ok(new { success = isRefunded });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error refunding PhonePe payment: {ex.Message}");
                return StatusCode(500, new { success = false, message = "Refund failed" });
            }
        }
    }

    public class PaymentInitiateRequest
    {
        public string? OrderId { get; set; }
        public decimal Amount { get; set; }
        public string? CustomerId { get; set; }
    }

    public class PaymentVerifyRequest
    {
        public string? TransactionId { get; set; }
        public string? MerchantTransactionId { get; set; }
    }

    public class PaymentRefundRequest
    {
        public string? MerchantTransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}
