using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Web.UI;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["xuser"] != null)
            {
                LoadOrders();

                //cartqty.InnerText = Request.Cookies["xcartqty"].Value;
            }
            else
            {
                Response.Redirect("login.aspx?mya=a");
            }
        }


    }

    public void LoadOrders()
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("select * from [xUser Details] where [Mobile No]='" + (string)Session["xuser"] + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    txtmob.Value = dr["Mobile No"].ToString();
                    txtname.Value = dr["Name"].ToString();
                    txtemail.Value = dr["Email"].ToString();
                    txtaddress1.Value = dr["Address1"].ToString();
                    txtaddess2.Value = dr["Address2"].ToString();
                    txtcity.Value = dr["City"].ToString();
                    txtpin.Value = dr["Pincode"].ToString();
                }
            }

            cn.Close();
        }
        catch (Exception ex)
        {
            //  WebMsgBox.Show(ex.Message);
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
       
    }

    public void Requester(String url)
    {
        try
        {
            var myRequest = (HttpWebRequest)WebRequest.Create(url);
            //set up web request...
            ThreadPool.QueueUserWorkItem(o => { myRequest.GetResponse(); });

            //Go2(url);
        }
        catch (Exception)
        {
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        string pwd = Text1.Value;

        string pwd2 = Text2.Value;

        if (pwd == pwd2)
        {

            string data = "";
            var cn = new SqlConnection(Program.Connection);
            try
            {
                cn.Open();
                var cmd = new SqlCommand("update [xuser details] set [Password]='" + pwd + "' where [Mobile No]='" + (string)Session["xuser"] + "'", cn);
                cmd.ExecuteNonQuery();
                cn.Close();

                string data2 = "HEY!! PASSWORD UPDATE.%0A%0AYOUR USERNAME: " + txtmob.Value + "%0AYOUR NEW PASSWORD: " + pwd;

                //var sUrl2 = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=F8rGlPbmBkSQK4ZlPFNSDA&senderid=IRFOOD&channel=2&DCS=0&flashsms=0&number=" + txtmob.Value + "&text=" + data2 + "&route=13";

               // Requester(sUrl2);

                WebMsgBox.Show("Password Updated");
            }
            catch (Exception ex)
            {
                //  WebMsgBox.Show(ex.Message);
            }
        }
        else
        {
            WebMsgBox.Show("New & Confirm Password Must be Same");
        }
    }

    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("update [xuser details] set [Name]='"+txtname.Value+"',[Email]='"+txtemail.Value+"',[Address1]='"+txtaddress1.Value+"',[Address2]='"+txtaddess2.Value+"',[City]='"+txtcity.Value+"',[PinCode]='"+txtpin.Value+"' where [Mobile No]='" + (string)Session["xuser"] + "'", cn);
            cmd.ExecuteNonQuery();

            cn.Close();

            WebMsgBox.Show("Details updated");
        }
        catch (Exception ex)
        {
            //  WebMsgBox.Show(ex.Message);
        }
    }
}