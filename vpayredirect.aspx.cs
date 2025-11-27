using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

public partial class vpayinit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string uy = "";
        try
        {
            List<KeyValuePair<string, string>> postparamslist = new List<KeyValuePair<string, string>>();

            for (int i = 0; i < Request.Form.Keys.Count; i++)
            {
                KeyValuePair<string, string> postparam = new KeyValuePair<string, string>(Request.Form.Keys[i], Request.Form[i]);


                postparamslist.Add(postparam);
            }


            string transactionid = "";
            string paymentid = "";
            string requestid = "";
            string responsecode = "";
            string status = "";



            foreach (KeyValuePair<string, string> param in postparamslist)
            {



                if (param.Key == "ResponseCode")
                {
                    responsecode = param.Value.ToString();
                }

                if (param.Key == "MerchantRefNo")
                {
                    transactionid = param.Value.ToString();
                }

                if (param.Key == "PaymentID")
                {
                    paymentid = param.Value.ToString();
                }

                if (param.Key == "RequestID")
                {
                    requestid = param.Value.ToString();
                }
            }
            uy = transactionid;
            if (responsecode == "0")
            {
                SaveXData(requestid, paymentid, "Credit", transactionid);
            }
            else
            {
                SaveXData(requestid, paymentid, "Failed", transactionid);
            }

            Response.Redirect("vpayverify.aspx?tranid=" + transactionid, false);
        }
        catch (Exception)
        {
            Response.Redirect("vpayverify.aspx?tranid=" + uy, false);
        }
    }

    public void SaveXData(string pgrequestid, string pgpaymentid, string status, string transaction)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {

            cn.Open();

            var cmdt = new SqlCommand("select [id] from [PG Provider Instamojo] where [transaction]='"+transaction+"' order by [id] desc",cn);
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
}