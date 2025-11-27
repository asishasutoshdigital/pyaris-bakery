using CCA.Util;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

public partial class PaymentGateway : System.Web.UI.Page
{
    public static string transaction = "";
    public static string name = "";
    public static string amount = "";
    public static string phone = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            transaction = Request.Cookies["ytransaction"].Value;
            name = Request.Cookies["yname"].Value;
            amount = Request.Cookies["yamount"].Value;
            phone = Request.Cookies["ydeliveryphone"].Value;

            SaveXData(transaction, Request.Cookies["yamount"].Value, Request.Cookies["yname"].Value,
                  Request.Cookies["yphone"].Value, Request.Cookies["yemail"].Value);

            if (Program.PaymentGateway == "CCAvenue")
            {
                string ccaRequest = "";
                var workingKey = Program.CCAvenue_workingKey;
                CCACrypto ccaCrypto = new CCACrypto();
                ccaRequest = ccaRequest + "merchant_id=" + Program.CCAvenue_merchantid + "&";
                ccaRequest = ccaRequest + "currency=" + Program.CCAvenue_currency + "&";
                ccaRequest = ccaRequest + "amount=" + Request.Cookies["yamount"].Value + "&";
                ccaRequest = ccaRequest + "language=" + Program.CCAvenue_language + "&";
                ccaRequest = ccaRequest + "billing_country=" + Program.CCAvenue_billingcountry + "&"; 
                ccaRequest = ccaRequest + "billing_tel=" + Request.Cookies["yphone"].Value + "&";
                ccaRequest = ccaRequest + "billing_email=" + Request.Cookies["yemail"].Value + "&";
                ccaRequest = ccaRequest + "delivery_tel=" + Request.Cookies["ydeliveryphone"].Value + "&";
                //ccaRequest = ccaRequest + "integration_type=" + "iframe_normal" + "&";
                ccaRequest = ccaRequest + "redirect_url=" + Program.CCAvenue_redirecturl + "&"; 
                ccaRequest = ccaRequest + "cancel_url=" + Program.CCAvenue_cancelurl + "&"; 
                ccaRequest = ccaRequest + "order_id=" + transaction;
                string strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
                var access = Program.CCAvenue_access;
                encRequest.Value = strEncRequest;
                var stringda = ccaCrypto.Decrypt(strEncRequest, workingKey);
                access_code.Value = access;
                pgName.Value = "CCAvenue";
            }
            else
            {
                pgName.Value = "PhonePe";
            }
        }
        catch (Exception ex)
        {
            WebMsgBox.Show("Error: " + ex.StackTrace);
        }
    }

    [System.Web.Services.WebMethod]
    public static PhonePeResponse GetPhonePeUrl()
    {
        PhonePePayload payload = new PhonePePayload();
        payload.merchantId = PhonepeConstants.PhonepeMerchantId;
        payload.merchantTransactionId = Randompin.Generate(10) + "_" + transaction;
        payload.merchantUserId = name;
        payload.amount = (float.Parse(amount) * 100).ToString();
        payload.redirectUrl = Program.PhonepeCallbackUrl;
        payload.redirectMode = "POST";
        payload.callbackUrl = Program.PhonepeCallbackUrl;
        payload.paymentInstrument = new PaymentInstrument() { type = "PAY_PAGE" };
        payload.mobileNumber = phone;
        var serialzedPayload = JsonConvert.SerializeObject(payload);
        var encodedReq = Convert.ToBase64String(Encoding.UTF8.GetBytes(serialzedPayload));
        var hashValue = ComputeSha256Hash(encodedReq + "/pg/v1/pay" + PhonepeConstants.PhonepeSaltKey) + "###" + PhonepeConstants.PhonepeSaltIndex;

        PhonePeRequest request = new PhonePeRequest() { request = encodedReq };
        RestClient restClient = new RestClient(PhonepeConstants.PhonepeBaseUrl);
        var req = new RestRequest(PhonepeConstants.PhonepePayEndPoint, Method.Post);
        req.AddHeader("Content-Type", "application/json");
        req.AddHeader("X-VERIFY", hashValue);
        req.AddHeader("accept", "application/json");
        req.AddParameter("application/json", JsonConvert.SerializeObject(request), ParameterType.RequestBody);

        var response = restClient.ExecuteAsync(req).Result;
        WebMsgBox.Show("Error: " + response.Content);
        var phonePeResponse = JsonConvert.DeserializeObject<PhonePeResponse>(response.Content);
        return phonePeResponse;
    }

    public void SaveXData(string transaction, string amount, string name, string phone,
      string email)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd =
                new SqlCommand(
                    "insert into [PG Provider Instamojo] values ('" + DateTime.Now.ToString("dd-MM-yyyy") + "','" +
                    DateTime.Now.ToString("HH:mm") + "','" + transaction + "','','','','','PENDING','" +
                    amount + "','0','" + name + "','" + phone + "','" + email + "','','','','','0')", cn);
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception ex)
        {
            // Response.Write("-1");
        }
    }

    public static string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}