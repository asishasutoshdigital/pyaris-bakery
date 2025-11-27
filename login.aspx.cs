using System;
using System.Data.SqlClient;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserNameErrorText.InnerText = "";
        if (Session["xtran"] == null || Session["xtran"].ToString() == "")
        {
            Session["xtran"] = Randompin.Generate(20);
            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
        }
        if (!IsPostBack)
        {
            PasswordSection.Visible = false;
            LoginButtonSection.Visible = false;
        }
    }


    protected void ContinueBtnClicked(object sender, EventArgs e)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            if (Username.Value != "" && Username.Value.Length == 10)
            {
                cn.Open();
                var cmd = new SqlCommand("select * from [xUser Details] where [Mobile No]='" + Username.Value + "'", cn);
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        PasswordSection.Visible = true;
                        LoginButtonSection.Visible = true;
                        ContinueButtonSection.Visible = false;
                        Username.Attributes.Add("readonly", "readonly");
                    }
                }
                else
                {
                    Application["MobileNumber"] = Username.Value.ToString();
                    Response.Redirect("Register.aspx");
                }
                cn.Close();
            }
            else
            {
                UserNameErrorText.InnerText = "Please enter valid Mobile number";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void LoginBtnClicked(object sender, EventArgs e)
    {
        LoginandProceed();
    }

    public void LoginandProceed()
    {
        string ty = "";
        ErrorText.InnerText = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            if (Username.Value != "" && Password.Value != "")
            {
                cn.Open();
                var cmd = new SqlCommand("select * from [xUser Details] where [Mobile No]='" + Username.Value + "' and [Password]='" + Password.Value + "' COLLATE Latin1_General_CS_AS ", cn);
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        Session["xuser"] = dr[1].ToString();
                        Session["xusername"] = dr[2].ToString();
                        ty = "1";
                    }
                }
                else
                {
                    ErrorText.InnerText = "The mobile number and password combination provided is not valid";
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
            else
            {
                ErrorText.InnerText = "Please Enter Mobile number and Password";
            }
        }
        catch (Exception e)
        {

        }
    }

    protected void RegisterBtnClicked(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }
}