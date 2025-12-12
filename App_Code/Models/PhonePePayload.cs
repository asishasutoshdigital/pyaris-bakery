public class PhonePePayload
{
    public string merchantId { get; set; }
    public string merchantTransactionId { get; set; }
    public string merchantUserId { get; set; }
    public string amount { get; set; }
    public string redirectUrl { get; set; }
    public string redirectMode { get; set; }
    public string callbackUrl { get; set; }
    public string mobileNumber { get; set; }
    public PaymentInstrument paymentInstrument { get; set; }
}

public class PaymentInstrument
{
    public string type { get; set; }
}

public class PhonePeRequest
{
    public string request { get; set; }
}



public class PhonePeRefundRequest
{

    public string merchantId { get; set; }
    public string merchantUserId { get; set; }
    public string originalTransactionId { get; set; }
    public string merchantTransactionId { get; set; }
    public string amount { get; set; }
    public string callbackUrl { get; set; }
}

public class PhonePeResponse
{
    public bool success { get; set; }
    public string code { get; set; }
    public string message { get; set; }
    public PhonePeResponseData data { get; set; }

}

public class PhonePeResponseData
{
    public string merchantId { get; set; }
    public string merchantTransactionId { get; set; }
    public string transactionId { get; set; }
    public PaymentInstrumentResponse instrumentResponse { get; set; }
}

public class PaymentInstrumentResponse
{
    public string type { get; set; }
    public RedirectInfo redirectInfo { get; set; }
}

public class RedirectInfo
{
    public string url { get; set; }
    public string method { get; set; }
}

public class PhonePeStatusResponse
{
    public bool success { get; set; }
    public string code { get; set; }
    public string message { get; set; }
    public PhonePeStatusResponseData data { get; set; }

}

public class PhonePeStatusResponseData
{
    public string merchantId { get; set; }
    public string merchantTransactionId { get; set; }
    public string transactionId { get; set; }
    public string amount { get; set; }
    public string State { get; set; }
    public string responseCode { get; set; }
    public PaymentStatusInstrumentResponse paymentInstrument { get; set; }
}

public class PaymentStatusInstrumentResponse
{
    public string type { get; set; }
    public string cardType { get; set; }
    public string pgTransactionId { get; set; }
    public string pgServiceTransactionId { get; set; }
    public string bankTransactionId { get; set; }
    public string pgAuthorizationCode { get; set; }
    public string arn { get; set; }
    public string utr { get; set; }
    public string bankId { get; set; }
}