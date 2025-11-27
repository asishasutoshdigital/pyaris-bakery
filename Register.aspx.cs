using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

        if (Session["xtran"] == null || Session["xtran"].ToString() == "")
        {
            Session["xtran"] = Randompin.Generate(20);
            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
        }
        if (!IsPostBack)
        {
            var mobileNumber = Application["MobileNumber"];
            if (mobileNumber != null)
            {
                txtmob.Value = mobileNumber.ToString();
                txtmob.Attributes.Add("readonly", "readonly");
            }
        }
    }

    protected void RegisterBtnClicked(object sender, EventArgs e)
    {
        if (txtmob.Value != "" && txtemailid.Value != "" && txtname.Value.Trim() != "" && txtpwd.Value.Trim() != "")
        {
            if (IsEmail(txtemailid.Value))
            {
                RegisterandProceed();
            }
            else
            {
                ErrorText.InnerText = "Email address is invalid.";
            }
        }
        else
        {
            ErrorText.InnerText = "Mobile Number, Password, Name & Email are Required.";
        }
    }

    public void RegisterandProceed()
    {
        string ty = "";
        //string pwd = Randompin.Generate(6);
        //var cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("select [id] from [xUser Details] where [Mobile No]='" + txtmob.Value + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                ErrorText.InnerText = "Mobile Number entered has been already registered. User another number.";
            }
            else
            {

                //var cmdx = new SqlCommand("insert into [Xuser details] values ('" + txtmob.Value + "','" + txtname.Value + "','" + txtemail.Value + "','" + txtaddress1.Value + "','" + txtaddess2.Value + "','" + txtcity.Value + "','" + txtpin.Value + "','" + pwd + "')", cn);
                var cmdx = new SqlCommand("insert into [Xuser details] values ('" + txtmob.Value + "','" + txtname.Value + "','" + txtemailid.Value + "','" + txtaddress1.Value + "','" + txtaddress2.Value + "','" + txtcity.Value + "','" + txtpin.Value + "','" + txtpwd.Value + "')", cn);
                cmdx.ExecuteNonQuery();

                //string data2 = "HEY!! THANKYOU FOR REGISTERING WITH PYARIS BAKERY.%0A%0AYOUR USERNAME: " + txtmob.Value + "%0AYOUR PASSWORD: " + pwd;

                ////var sUrl2 = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=F8rGlPbmBkSQK4ZlPFNSDA&senderid=IRFOOD&channel=2&DCS=0&flashsms=0&number=" + txtmob.Value + "&text=" + data2 + "&route=13";

                //string data2e = "HEY!! THANKYOU FOR REGISTERING WITH PYARIS BAKERY. YOUR USERNAME: " + txtmob.Value + ". YOUR PASSWORD: " + pwd;


                string data2 = "Hi " + txtname.Value + ", Thank you for registering at Paris Bakery. Your New Password is " + txtpwd.Value + ".%0ARequest you to change your password on first login.%0AStart shopping and win Loyalty Points. T and C apply%0A%0AFor any issues/ bulk order / catering requests, please contact us: 8018114444(8AM - 9PM)%0A%0ARegards,%0ATeam - Paris Bakery";

                string data2e = "Hi " + txtname.Value + ", Thank you for registering at Paris Bakery. Your New Password is " + txtpwd.Value + ".\nRequest you to change your password on first login.\nStart shopping and win Loyalty Points. T & C apply\n\nFor any issues/ bulk order / catering requests, please contact us: 8018114444(8AM - 9PM)\n\nRegards,\nTeam - Paris Bakery";

                sendSMS.Send(txtmob.Value, data2);

                sendemail.Gox(txtemailid.Value, "WELCOME TO PYARIS BAKERY", data2e);

                Session["xuser"] = txtmob.Value;
                Session["xusername"] = txtname.Value;

                ty = "1";

            }
            cn.Close();

            if (ty == "1")
            {
                if (Request.QueryString["xs"] != null && Request.QueryString["xs"] != "")
                {
                    Response.Redirect("sparknext.aspx");
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    public const string MatchEmailPattern =
           @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";


    public static bool IsEmail(string email)
    {
        if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        else return false;
    }
}