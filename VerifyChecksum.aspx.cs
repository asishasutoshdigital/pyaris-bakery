using CCA.Util;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
//using paytm;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public partial class VerifyChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Program.PaymentGateway == "CCAvenue")
        {
            if (Request.Form.AllKeys.Length > 0)
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string uy = "";
                try
                {
                    string paytmChecksum = "", responseString = "";
                    bool Isvalidate = false;


                    foreach (string key in Request.Form.Keys)
                    {
                        if (Request.Form[key].ToUpper().Contains("REFUND") || Request.Form[key].Contains("|"))
                        {
                            parameters.Add(key.Trim(), "");
                        }
                        else
                        {
                            parameters.Add(key.Trim(), Request.Form[key].Trim());
                        }
                    }
                    string transactionid = "";
                    string paymentid = "";
                    string requestid = "";
                    string responsecode = "";
                    string deliveryTel = "";
                    string status = "";
                    if (parameters.ContainsKey("encResp"))
                    {
                        var response = parameters["encResp"];
                        CCACrypto ccaCrypto = new CCACrypto();
                        string encResponse = ccaCrypto.Decrypt(response, "486B935EF27AF33FB324D0BB498F0939");

                        NameValueCollection dict = new NameValueCollection();
                        string[] segments = encResponse.Split('&');
                        foreach (string seg in segments)
                        {
                            string[] parts = seg.Split('=');
                            if (parts.Length > 0)
                            {
                                string Key = parts[0].Trim();
                                string Value = parts[1].Trim();
                                dict.Add(Key, Value);
                            }
                        }

                        var responseToDictionary = dict.AllKeys.ToDictionary(k => k, k => dict[k]);
                        foreach (var param in responseToDictionary)
                        {
                            if (param.Key == "order_status")
                            {
                                responsecode = param.Value.ToString();
                            }

                            if (param.Key == "order_id")
                            {
                                transactionid = param.Value.ToString();
                                requestid = param.Value.ToString();
                            }

                            if (param.Key == "tracking_id")
                            {
                                paymentid = param.Value.ToString();
                            }

                            if (param.Key == "delivery_tel")
                            {
                                deliveryTel = param.Value.ToString();
                                Response.Cookies["ydeliveryphone"].Value = param.Value.ToString();
                            }
                            if (!string.IsNullOrEmpty(paymentid) && !string.IsNullOrEmpty(transactionid) &&
                                !string.IsNullOrEmpty(requestid) && !string.IsNullOrEmpty(responsecode) &&
                                !string.IsNullOrEmpty(deliveryTel))
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (parameters.ContainsKey("CHECKSUMHASH"))
                        {
                            paytmChecksum = parameters["CHECKSUMHASH"];
                            parameters.Remove("CHECKSUMHASH");
                        }

                        //Isvalidate = CheckSum.verifyCheckSum(PaytmConstants.MERCHANT_KEY, parameters, paytmChecksum);
                        //Response.Write(Isvalidate);

                        List<KeyValuePair<string, string>> postparamslist = new List<KeyValuePair<string, string>>();

                        for (int i = 0; i < Request.Form.Keys.Count; i++)
                        {
                            KeyValuePair<string, string> postparam = new KeyValuePair<string, string>(Request.Form.Keys[i], Request.Form[i]);


                            postparamslist.Add(postparam);
                        }

                        foreach (KeyValuePair<string, string> param in postparamslist)
                        {
                            if (param.Key == "STATUS")
                            {
                                responsecode = param.Value.ToString();
                            }

                            if (param.Key == "ORDERID")
                            {
                                transactionid = param.Value.ToString();
                            }

                            if (param.Key == "TXNID")
                            {
                                paymentid = param.Value.ToString();
                            }

                            if (param.Key == "ORDERID")
                            {
                                requestid = param.Value.ToString();
                            }
                        }
                    }
                    uy = transactionid;
                    if (responsecode == "TXN_SUCCESS" || responsecode == "Success")
                    {
                        SaveXData(requestid, paymentid, "Credit", transactionid);
                    }
                    else
                    {
                        SaveXData(requestid, paymentid, "Failed", transactionid);
                    }

                    Response.Redirect("vpayverify.aspx?tranid=" + transactionid, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("vpayverify.aspx?tranid=" + uy, false);
                }
            }
        }
        else if (Program.PaymentGateway == "PhonePe")
        {
            var merchantTransactionId = "";
            var response = "";
            var phonePeResponse = new PhonePeStatusResponse();
            if (Request.Form.AllKeys.Length > 0)
            {

                string uy = "";
                try
                {

                    foreach (string key in Request.Form.Keys)
                    {
                        if (key == "response")
                        {
                            response = Request.Form[key].Trim();
                        }
                        if (key == "transactionId")
                        {
                            merchantTransactionId = Request.Form[key].Trim();
                        }
                    }
                    if (response != "")
                    {
                        byte[] data = Convert.FromBase64String(response);
                        string decodedResponse = Encoding.UTF8.GetString(data);

                        phonePeResponse = JsonConvert.DeserializeObject<PhonePeStatusResponse>(decodedResponse);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("vpayverify.aspx?tranid=" + phonePeResponse.data.merchantTransactionId, false);
                }
            }
            if (response == "" && merchantTransactionId != "")
            {
                var saltKey = PhonepeConstants.PhonepeSaltKey;
                var hashValue = ComputeSha256Hash("/pg/v1/status/" + PhonepeConstants.PhonepeMerchantId + "/" + merchantTransactionId + saltKey) + "###" + PhonepeConstants.PhonepeSaltIndex;

                RestClient restClient = new RestClient(PhonepeConstants.PhonepeBaseUrl);
                var req = new RestRequest(PhonepeConstants.PhonepeStatusEndPoint, Method.Get);
                req.AddHeader("X-MERCHANT-ID", PhonepeConstants.PhonepeMerchantId);
                req.AddHeader("X-VERIFY", hashValue);
                req.AddParameter("merchantId", PhonepeConstants.PhonepeMerchantId, ParameterType.UrlSegment);
                req.AddParameter("merchantTransactionId", merchantTransactionId, ParameterType.UrlSegment);
                var statusResponse = restClient.ExecuteAsync(req).Result;
                phonePeResponse = JsonConvert.DeserializeObject<PhonePeStatusResponse>(statusResponse.Content);
            }
            
            var transactionId = merchantTransactionId.Split('_')[1];
            if (phonePeResponse.success && phonePeResponse.code == "PAYMENT_SUCCESS")
            {
                SaveXData(merchantTransactionId, phonePeResponse.data.transactionId, "Credit", transactionId);
            }
            else
            {
                SaveXData(merchantTransactionId, phonePeResponse.data.transactionId, "Failed", transactionId);
            }

            Response.Redirect("vpayverify.aspx?tranid=" + transactionId, false);
        }
    }

    public void SaveXData(string pgrequestid, string pgpaymentid, string status, string transaction)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmdt = new SqlCommand("select [id] from [PG Provider Instamojo] where [transaction]='" + transaction + "' order by [id] desc", cn);
            var drt = cmdt.ExecuteReader();
            if (drt.HasRows)
            {
                if (drt.Read())
                {
                    if (status == "Credit")
                    {
                        var cmd =
                            new SqlCommand(
                                "update [PG Provider Instamojo] set [PG Payment ID]='" + pgpaymentid +
                                "',[Payment Status]='SUCCESS',[PG Request ID]='" + pgrequestid + "' where [id]='" +
                               drt[0].ToString() + "'", cn);
                        cmd.ExecuteNonQuery();

                        var cmdtotal = new SqlCommand("select coalesce (count([id]),0),coalesce (sum([Total Cost Price]),0),coalesce (sum([Total Sale Amount]),0),coalesce (sum([Total Tax Amount]),0) from [Xsales slave] where [Transaction]='" + transaction + "'", cn);
                        var drtotal = cmdtotal.ExecuteReader();
                        var user = "";
                        var address = "";
                        if (drtotal.Read())
                        {
                            var cmdxrtdel = new SqlCommand("select * from [XSales Pg Mid Process] where [Transaction]='" + transaction + "'", cn);
                            var drxrtdel = cmdxrtdel.ExecuteReader();
                            if (drxrtdel.HasRows)
                            {
                                if (drxrtdel.Read())
                                {
                                    user = drxrtdel["Phone"].ToString();
                                    var paybackpoint = drxrtdel["PaybackPoint"].ToString();
                                    var paybackamount = Math.Round(double.Parse(paybackpoint) * 0.5, 0);
                                    address = drxrtdel["Address"].ToString();
                                    double tytotal = double.Parse(drtotal[2].ToString());
                                    tytotal = tytotal - paybackamount + double.Parse(drxrtdel["DeliveryCharge"].ToString());
                                    if (!IsPostBack)
                                    {
                                        var cmd1 = new SqlCommand("select * from [Xsales master] where [Transaction]='" + transaction + "'", cn);
                                        var dr1 = cmd1.ExecuteReader();
                                        if (!dr1.HasRows)
                                        {
                                            var cmdinsertmaster = new SqlCommand("insert into [Xsales master] values ('" + transaction + "','" +
                                                DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH:mm") + "','" +
                                                drtotal[0] + "','" + drtotal[1] + "','" + drtotal[2] + "','" + drtotal[3] + "','" + Response.Cookies["Discount_Provider"].Value + "','" + Response.Cookies["Discount"].Value + "','" +
                                                user + "','" + paybackamount + "','" + tytotal + "','ONLINE','" + transaction +
                                                "','" + user + "','" + drxrtdel["Delivery"].ToString() + "','" + drxrtdel["Date"].ToString() + "','" + drxrtdel["Location"].ToString() + "','" + drxrtdel["Address"].ToString() +
                                                "','ORDER_RECEIVED','','" + drxrtdel["Timeof"].ToString() + "','" + drxrtdel["DeliveryCharge"].ToString() + "','WEBSITE')", cn);
                                            cmdinsertmaster.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }

                        string cgid = "1111";
                        var cmd1x = new SqlCommand("select [ID] from [xSales MAster] where [Transaction]='" + transaction + "'", cn);
                        var dr1x = cmd1x.ExecuteReader();
                        if (dr1x.HasRows)
                        {
                            if (dr1x.Read())
                            {
                                cgid = dr1x[0].ToString();
                            }
                        }
                        var cmdins = new SqlCommand("update [xSales MAster] set [Order no]='" + cgid + "' where [Transaction]='" + transaction + "'", cn);
                        cmdins.ExecuteNonQuery();

                        var cmdupd = new SqlCommand("update [xSales slave] set [Order no]='" + cgid + "' where [Transaction]='" + transaction + "'", cn);
                        cmdupd.ExecuteNonQuery();

                        var cmdins4 = new SqlCommand("update [PG Provider Instamojo] set [ORDER]='" + cgid + "' where [id]='" + drt[0].ToString() + "'", cn);
                        cmdins4.ExecuteNonQuery();

                        Session["xcartqty"] = "0";
                        Session["xpaybackpoint"] = "0";
                        Response.Cookies["Discount_Provider"].Value = "";
                        Response.Cookies["Discount"].Value = "0";

                        Notifications.SaveNotification("A NEW WEB ORDER : " + cgid + ", HAS BEEN RECIVED.");

                        string name = Util.Getname(user);
                        string deliveryTel = "";//Request.Cookies["ydeliveryphone"].Value;

                        if (!string.IsNullOrEmpty(address))
                        {
                            var addressArray = address.Split(new char[] { ',' });
                            var deliveryContact = addressArray.Where(x => x.Contains("DELIVERYCONTACT")).FirstOrDefault();
                            if (!string.IsNullOrEmpty(deliveryContact))
                            {
                                deliveryTel = deliveryContact.Split(new char[] { ':' })[1].Trim();
                            }
                        }
                        if (string.IsNullOrEmpty(deliveryTel))
                        {
                            deliveryTel = user;
                        }

                        //string data2 = "Hi " + name + ", Thank you for placing your order with us. Your Confirmed Order ID is " + cgid + ".%0A";
                        string data2 = "Hi " + name + ", Thank you for placing your order with us. Your Order ID is " + cgid + ", Paris Bakery (Jam foods)";
                        sendSMS.Send(deliveryTel, data2,Program.SMS_Template_Order_Placing);

                        //string data23 = "Hi " + name + ", Thank you for placing your order with us. Your Confirmed Order ID is " + cgid + ".\n";
                        string data23 = "Hi " + name + ", \nThank you we have received your order. Your Order ID is " + cgid + ".\nParis Bakery (Jam foods)";

                        sendemail.Gox(sendemail.Fetchemail(user), "ORDER RECEIVED", data23);

                        //Inform Pyaris
                        string pyarisMessage = "A WEB ORDER : " + cgid + ", HAS BEEN Made. User: " + name + " - " + user + " | Contact - " + deliveryTel;
                        sendSMS.Send(Program.Admin_SecondaryPhoneNumber, pyarisMessage,Program.SMS_Template_NewOrder);
                        sendSMS.Send(Program.Admin_PhoneNumber, pyarisMessage, Program.SMS_Template_NewOrder);
                        sendemail.Gox(Program.Customercare_Emailid, "WEB ORDER RECEIVED", pyarisMessage);
                    }
                    if (status == "Failed")
                    {
                        var cmd =
                            new SqlCommand(
                                "update [PG Provider Instamojo] set [PG Payment ID]='" + pgpaymentid +
                                "',[Payment Status]='FAILED',[PG Request ID]='" + pgrequestid + "' where [id]='" +
                               drt[0].ToString() + "'", cn);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            cn.Close();
        }
        catch (Exception ex)
        {

        }
    }


    public string ComputeSha256Hash(string rawData)
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