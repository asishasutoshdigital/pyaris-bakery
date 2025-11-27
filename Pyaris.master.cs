using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class Pyaris : System.Web.UI.MasterPage
{
    public bool IsLoggedIn = false;
    public bool IsAdmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["xcartqty"] != null)
        {
            cartqty.InnerText = mobilecartqty.InnerText = Session["xcartqty"].ToString();
        }
        else
        {
            Session["xcartqty"] = "0";
            cartqty.InnerText = mobilecartqty.InnerText = Session["xcartqty"].ToString();
        }
        List<string> adminPages = new List<string>(new string[] { "Products", "StoreOrders", "PendingOrders", "Customers", "EditProducts", "ServiceReport" });
        string pageName = Path.GetFileName(Request.Path).Split('.')[0];
        var isAdminPage = adminPages.Any(x => x.Contains(pageName));

        if (isAdminPage && (string)Session["xuser"] != Program.Admin_PhoneNumber)
        {
            WebMsgBox.Show("Login to access this page");
            Response.Redirect("login.aspx");
        }

        if (Session["xuser"] != null)
        {
            loginlink.InnerHtml = "<img src=\"images/svg-icons/user.svg\" alt=\"My Account\" /><span>" + (string)Session["xusername"] + " | My Account</span><i class=\"ddl-switch fa fa-angle-down\"></i>";
            usernameoption.InnerHtml = "Welcome " + (string)Session["xusername"];
            if ((string)Session["xuser"] != Program.Admin_PhoneNumber)           
            {
                StoreOrders.Visible = false;
                Products.Visible = false;
                Customers.Visible = false;
                ServiceReport.Visible = false;
                PendingOrders.Visible = false;
                UpdateBanner.Visible = false;
            }
            else
            {
                TrackOrders.Visible = false;
                MyOrders.Visible = false;
                MyCart.Visible = false;
                IsAdmin = true;
            }
            IsLoggedIn = true;
        }
        else
        {
            loginmenu.InnerHtml = "<a class=\"links\" href=\"login.aspx\"><img src=\"images/svg-icons/user.svg\" alt=\"My Account\" /><span>Login/Signup</span></a>";
        }
        payBackLink.Visible = Program.isPaybackAvailable;
    }
}
