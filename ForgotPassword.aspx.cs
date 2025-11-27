using System;
using System.Data.SqlClient;
using System.Web.UI;

public partial class ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xtran"] == null || Session["xtran"].ToString() == "")
        {
            Session["xtran"] = Randompin.Generate(20);

            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
        }
    }

    protected void ForgotPasswordBtnClicked(object sender, EventArgs e)
    {
        ForgotPasswordClicked();
    }

    public void ForgotPasswordClicked()
    {
        xdrf.InnerText = "";
        string ty = "";
        string email = "";
        string name = "";
        var cn = new SqlConnection(Program.Connection);
        // var cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["myCon"].ConnectionString);
        try
        {
            if (UserName.Value != "")
            {
                cn.Open();
                var cmd = new SqlCommand("select [Password],[email],[Name] from [xUser Details] where [Mobile No]='" + UserName.Value + "'", cn);
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        ty = dr[0].ToString();
                        email = dr[1].ToString();
                        name = dr[2].ToString();
                    }
                }
                else
                {
                    xdrf.InnerText = "Username is incorrect.";
                }
                cn.Close();
                if (ty != "")
                {
                    // string data2 = "HEY!! NOT ONLY YOU BUT EVEN WE FORGET IT SOMETIMES :) SO DON'T WORRY !!.%0A%0AYOUR USERNAME: " + Text3.Value + "%0AYOUR PASSWORD: " + ty + "%0A%0APYARIS BAKERY";


                    //  string data2e = "HEY!! NOT ONLY YOU BUT EVEN WE FORGET IT SOMETIMES :) SO DON'T WORRY !!. YOUR USERNAME: " + Text3.Value + ". YOUR PASSWORD: " + ty + ". PYARIS BAKERY";



                    string data2 = "Hi " + name + ",  Your current password is " + ty + ".%0ALog in to your account and explore our delectable menu.%0A%0AFor any issues / bulk order / catering requests, please contact us: 8018114444(8AM - 9PM)%0A%0ARegards,%0ATeam - Paris Bakery";


                    string data2e = "Hi " + name + ",  Your current password is " + ty + ".\nLog in to your account and explore our delectable menu.\n\nFor any issues / bulk order / catering requests, please contact us: 8018114444(8AM - 9PM)\n\nRegards,\nTeam - Paris Bakery";



                    //var sUrl2 = "http://login.smsgatewayhub.com/api/mt/SendSMS?APIKey=F8rGlPbmBkSQK4ZlPFNSDA&senderid=IRFOOD&channel=2&DCS=0&flashsms=0&number=" + Text3.Value + "&text=" + data2 + "&route=13";
                    sendSMS.Send(UserName.Value, data2);

                    sendemail.Gox(email, "PASSWORD RECOVERY - PARIS BAKERY", data2e);

                    xdrf.InnerText = "Password Sent To Your Registered Mobile No & Email ID.";
                }
            }
            else
            {
                xdrf.InnerText = "Mobile Number cannot be empty.";
            }
        }
        catch (Exception)
        {

        }
    }
}