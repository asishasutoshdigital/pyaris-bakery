using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        try
        {
            var transaction = Request.Cookies["ytransaction"].Value;




            SaveXData(transaction, Request.Cookies["yamount"].Value, Request.Cookies["yname"].Value,
                Request.Cookies["yphone"].Value, Request.Cookies["yemail"].Value);


            string Url = "vpayhash.aspx";
            string Method = "post";
            string FormName = "form1";


            NameValueCollection FormFields = new NameValueCollection();

            FormFields.Add("account_id", "18925");
            FormFields.Add("channel", "0");
            FormFields.Add("currency", "INR");
            FormFields.Add("reference_no", transaction);
            FormFields.Add("amount", Request.Cookies["yamount"].Value);
            FormFields.Add("description", "ORDER");
            FormFields.Add("name", Request.Cookies["yname"].Value);
            FormFields.Add("address", "india");
            FormFields.Add("city", "kolkata");
            FormFields.Add("state", "wb");
            FormFields.Add("postal_code", "700001");
            FormFields.Add("country", "IN");
            FormFields.Add("email", Request.Cookies["yemail"].Value);
            FormFields.Add("phone", Request.Cookies["yphone"].Value);
            FormFields.Add("mode", "LIVE");
            FormFields.Add("return_url", "https://YOURIP/vpayredirect.aspx");
            FormFields.Add("ship_name", Request.Cookies["yname"].Value);
            FormFields.Add("ship_address", "india");
            FormFields.Add("ship_city", "kolkata");
            FormFields.Add("ship_state", "wb");
            FormFields.Add("ship_country", "IN");
            FormFields.Add("ship_phone", Request.Cookies["yphone"].Value);
            FormFields.Add("algo", "MD5");
            FormFields.Add("ship_postal_code", "700001");
            FormFields.Add("name_on_card", "null");
            FormFields.Add("card_number", "null");
            FormFields.Add("card_expiry", "null");
            FormFields.Add("card_cvv", "null");

            Response.Clear();
            Response.Write("<html><head>");
            Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            for (int i = 0; i < FormFields.Keys.Count; i++)
            {
                Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", FormFields.Keys[i], FormFields[FormFields.Keys[i]]));
            }
            Response.Write("</form>");
            Response.Write("</body></html>");
            Response.End();

            // Response.Redirect("" + pgid + "?embed=form");

        }
        catch (Exception ex)
        {
            Response.Write("REDIRECTING ... ");
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