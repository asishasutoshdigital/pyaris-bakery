using System;
using System.Collections.Generic;
using System.Data.SqlClient;
//using paytm;
public partial class GenerateChecksum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var transaction = Request.Cookies["ytransaction"].Value;

        SaveXData(transaction, Request.Cookies["yamount"].Value, Request.Cookies["yname"].Value,
              Request.Cookies["yphone"].Value, Request.Cookies["yemail"].Value);


        Dictionary<string, string> parameters = new Dictionary<string, string>();

        //string Merchant_key = "WM6oIEqrRD#15QpL";
        //string MID = "NECTAR74530282405026";
        //string Website = "WEBPROD";
        parameters.Add("MerchantId", "167513");
        parameters.Add("AccessCode", "AVTY01FC68BX10YTXB");
        parameters.Add("Currency", "INR");
        parameters.Add("WorkingKey", "9E20D5E942B277FC1165E6A5CDD4AF75");
        parameters.Add("CancelUrl", "");
        parameters.Add("RedirectUrl", "");
        parameters.Add("Country", "India");
        parameters.Add("Language", "EN");
        parameters.Add("CCAvenueURL", "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction");
        parameters.Add("CALLBACK_URL", "http://Pyaribakery.com/VerifyChecksum.aspx");
        parameters.Add("Contact", Request.Cookies["yphone"].Value);
        parameters.Add("OrderId", transaction);
        parameters.Add("Amount", Request.Cookies["yamount"].Value);


        //parameters.Add("MID", MID);
        //parameters.Add("CHANNEL_ID", "WEB");
        //parameters.Add("INDUSTRY_TYPE_ID", "Retail109");
        //parameters.Add("WEBSITE", Website);
        //string custId = Request.Cookies["yphone"].Value;
        string paytmURL = "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction";
        //parameters.Add("CALLBACK_URL", "https://Pyaribakery.com/VerifyChecksum.aspx");
        //parameters.Add("CUST_ID", Request.Cookies["yphone"].Value);
        //parameters.Add("ORDER_ID", transaction);
        //parameters.Add("TXN_AMOUNT", Request.Cookies["yamount"].Value);

        try
        {
            //string checksum = paytm.CheckSum.generateCheckSum(Merchant_key, parameters);

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></cente>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";

            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }

            // outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            Response.Write(outputHTML);

        }
        catch (Exception ex)
        {
            Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
        }




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


}