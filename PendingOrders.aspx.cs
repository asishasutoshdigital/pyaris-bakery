using Newtonsoft.Json;
using RestSharp;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Services;

public partial class PendingOrders: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd1 = new SqlCommand("SELECT  sm.*, SUBSTRING((SELECT '|' +  'Menu: ' + [Menu Name] + ' Quantity: ' + CAST(CAST([Quantity] AS DECIMAL(20)) AS VARCHAR(20))+ ' Weight: ' + [Weight] + ' Customer Flavour: ' + [Customer Flavour] FROM [XSales Slave] WHERE [Order No] = sm.[Order No] FOR XML PATH ('')), 2, 1000) AS Items FROM[XSales Master] AS sm where sm.Status ='ORDER_RECEIVED' ORDER BY sm.[ID] DESC", cn);
            var allOrders = cmd1.ExecuteReader();
            data += "<div class=\"col-md-12 table-responsive OrdersTableContainer\"><table id=\"OrdersTable\" class=\"table table-condensed tableContainer\"><thead><tr><th>Order decision &emsp;</th><th>Order No</th><th>Order Date</th><th>Status</th><th class=\"Addr-Order-Contiainer\">Items</th><th>User</th><th>Contact</th><th>Delivery Type</th><th>Delivery Date</th><th>Delivery Time</th><th>Total Items</th><th>Net Total</th><th>Pay Mode</th><th>Outlet</th><th class=\"Addr-Order-Contiainer\">Address</th><th>Source</th></tr></thead><tbody>";
            if (allOrders.HasRows)
            {
                while (allOrders.Read())
                {
                    var contactNumber = "";
                    var address = allOrders[19].ToString().Split(',');
                    if (address.Length > 0)
                    {
                        var lastString = address[address.Length - 1].Split(':');
                        if (lastString.Length > 1 && lastString[0].Trim() == "DELIVERYCONTACT")
                        {
                            contactNumber = lastString[1].Trim();
                        }
                    }
                    var displayAddress = "";
                    if (allOrders[16].ToString() == "HOME DELIVERY")
                    {
                        displayAddress = allOrders[19].ToString();
                    }

                    data += "<tr><td><button class=\"ExportBtnGrid\" onclick =\"decisionRecord($(this).val(),\'Accepted\')\"  type=\"button\" value=\"" + allOrders[1].ToString() + "\">Approve</button> <button type=\"button\" class=\"ExportBtnGrid\" onclick = \"decisionRecord($(this).val(),\'Rejected\')\" value=\"" + allOrders[1].ToString() + "\">Reject</button> </td>" + "<td>" + allOrders[1].ToString() + "</td><td>" + allOrders[2].ToString() + "</td><td style=\"white-space: normal\">" + allOrders[20].ToString() + "</td><td  class=\"Addr-Order-Contiainer\">" + allOrders[25].ToString().Replace("|", "</br>") + "</td><td>" + allOrders[15].ToString() + "</td><td>" + contactNumber + "</td><td>" + allOrders[16].ToString() + "</td><td>" + allOrders[17].ToString() + "</td><td>" + allOrders[22].ToString() + "</td><td>" + allOrders[4].ToString() + "</td><td>" + allOrders[12].ToString() + "</td><td>" + allOrders[13].ToString() + "</td><td>" + allOrders[18].ToString() + "</td><td class=\"Addr-Order-Contiainer\">" + displayAddress + "</td><td>" + allOrders[24].ToString() + "</td></tr>";
                }
            }
            else
            {
                data += "<tr ><td colspan=\"16\" align=\"center\"><b> No Orders to Approve/Reject </b> </td></tr>";
            }
            data += "</tbody></table></div>";
            cn.Close();

        }
        catch (Exception ex)
        {
            data = "-2";
        }
        PendingOdr.InnerHtml = data;
    }

    [WebMethod]
    public static string ActionOrder(string Decision, string OrderNumber)
    {
        try
        {
            PendingOrders objstore = new PendingOrders();
            string transaction = HttpContext.Current.Session["xtran"].ToString();
            string user = HttpContext.Current.Session["xuser"].ToString();
            string CustomerUser = Util.GetCustomeruser(OrderNumber);
            if (Decision == "Accepted")
            {
                objstore.UpdateOrderStatus("PLACED", OrderNumber);
                objstore.SendEmailMessage(Decision, OrderNumber, CustomerUser);
                return "Order Approved";
            }
            else if (Decision == "Rejected")
            {
                if (objstore.Paymentreversal(OrderNumber, CustomerUser))
                {
                    if (!(objstore.UpdateOrderStatus("REJECTED", OrderNumber))) { throw new Exception("Database Update failed"); }
                    if (!(objstore.SendEmailMessage(Decision, OrderNumber, CustomerUser))) { throw new Exception("Send Email failed"); }
                    return "Order Rejected. Refund Success";
                }
                else
                {
                    if (!(objstore.UpdateOrderStatus("REFUND-FAILED ", OrderNumber))) { throw new Exception("Database Update failed"); }
                    if (!(objstore.SendEmailMessage("Refund Failed", OrderNumber, CustomerUser))) { throw new Exception("Send Email failed"); }
                    return "Order Rejected. Refund Failed";
                }
            }
            return "sucess";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public bool UpdateOrderStatus(string Orderstatus, string ordernumber)
    {
        try
        {
            var cn = new SqlConnection(Program.Connection);
            cn.Open();
            var cmdins = new SqlCommand(
                               "update [xSales MAster] set [Status]='" + Orderstatus + "' where [Order No]='" + ordernumber +
                               "'",
                               cn);
            cmdins.ExecuteNonQuery();
            cn.Close();

            return true;
        }
        catch (Exception) { return false; }
    }
    public void UpdateRefundStatus(string ordernumber, PhonePeStatusResponse peResponse)
    {
        try
        {
            var cn = new SqlConnection(Program.Connection);
            cn.Open();
            var cmdins = new SqlCommand(
                               "update [PG Provider Instamojo] set [Refund Status]='" + (peResponse.success ? "Success" : "Failed") + "'," +
                               "[Refund Amount]='" + peResponse.data.amount + "'," +
                               "[Refund Description]='" + peResponse.message + "'," +
                               "[Refund Type Details]='" + peResponse.data.transactionId + "'," +
                               "[Refund Type Code]='" + peResponse.data.responseCode + "'" +
                               "where [Order]='" + ordernumber +
                               "' and [Transaction]='" + peResponse.data.merchantTransactionId + "'",
                               cn);
            cmdins.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception)
        {

        }
    }

    public bool Paymentreversal(string ordernumber, string user)
    {
        try
        {
            PaymentDetails paymentdetails = new PaymentDetails();
            //get Payment Type 
            paymentdetails = Util.GetPaymentDetails(ordernumber);

            if (paymentdetails.Paymentmode == "ONLINE")
            {
                //WebMsgBox.Show("Are you sure to reject the order "+ ordernumber + " for ammount " + paymentdetails.Ammount);
                PhonePeRefundRequest payload = new PhonePeRefundRequest();
                payload.merchantId = PhonepeConstants.PhonepeMerchantId;
                payload.merchantTransactionId = Randompin.Generate(20);
                payload.originalTransactionId = paymentdetails.PhonepeTransactionNumber;
                payload.merchantUserId = user;
                payload.amount = paymentdetails.Amount;
                payload.callbackUrl =Program.PhonepeCallbackUrl;

                var serialzedPayload = JsonConvert.SerializeObject(payload);
                var encodedReq = Convert.ToBase64String(Encoding.UTF8.GetBytes(serialzedPayload));
                var hashValue = Util.ComputeSha256Hash(encodedReq + PhonepeConstants.PhonepeRefundEndPoint + PhonepeConstants.PhonepeSaltKey) + "###" + PhonepeConstants.PhonepeSaltIndex;

                PhonePeRequest request = new PhonePeRequest() { request = encodedReq };
                RestClient restClient = new RestClient(PhonepeConstants.PhonepeBaseUrl);
                var req = new RestRequest(PhonepeConstants.PhonepeRefundEndPoint, Method.Post);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("X-VERIFY", hashValue);
                req.AddParameter("application/json", JsonConvert.SerializeObject(request), ParameterType.RequestBody);

                var response = restClient.ExecuteAsync(req).Result;
                var phonePeResponse = JsonConvert.DeserializeObject<PhonePeStatusResponse>(response.Content);
                UpdateRefundStatus(ordernumber, phonePeResponse);
                if (phonePeResponse.success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool SendEmailMessage(string status, string ordernumber, string user)
    {
        try
        {
            Notifications.SaveNotification("A NEW WEB ORDER : " + ordernumber + ", HAS BEEN " + status + ".");
            string smsMessage = string.Empty, emailMesssage = string.Empty;
            string name = Util.Getname(user);
            if (status == "Accepted")
            {
                smsMessage = "Hi " + name + ", Order Update. Your Order " + ordernumber + " is Accepted & Under Processing, Paris Bakery(Jam Foods)";
                emailMesssage = "Hi " + name + "<br> Order Update. Your Order " + ordernumber + " is Accepted and under Processing.<br>Paris Bakery(Jam Foods)";
                //Inform Pyaris
                string pyarisMessage = "A New Web Order :" + ordernumber + ", Has Been Made. User: " + name + " - " + user;
                sendemail.Gox(Program.Customercare_Emailid, "WEB ORDER RECEIVED", pyarisMessage);
            }
            else if (status == "Rejected")
            {
                smsMessage = "Hi " + name + ",Order Update. Your Order is Rejected and the order amount is refunded. Please contact customer care " + Program.Customercare_Emailid + " for further details, Paris Bakery(Jam Foods)";
                emailMesssage = "Hi " + name + ",<br> Order Update. Your Order is Rejected and the order amount is refunded. Please contact customer care " + Program.Customercare_Emailid + " for further details. <br>Paris Bakery(Jam Foods)";
            }
            else if (status == "Refund Failed")
            {
                smsMessage = "Hi " + name + ", Order Update. Your Order is Rejected. Please contact customer care " + Program.Customercare_Emailid + " for refund, Paris Bakery(Jam Foods)";
                emailMesssage = "Hi " + name + ",<br> Order Update. Your Order is Rejected. Please contact customer care " + Program.Customercare_Emailid + " for refund.<br>Paris Bakery(Jam Foods)";               
            }
            sendSMS.Send(user, smsMessage, Program.SMS_Template_Order_Update);
            sendemail.Gox(sendemail.Fetchemail(user), "Order " + status, emailMesssage);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}