using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class about : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["xcartqty"] != null)
        {
            string x = Request.Cookies["xcartqty"].Value;

            if (x == "0")
            {
                xcart.InnerText = "ONLINE MENU";
            }
            else
            {
                xcart.InnerText = x + " ITEMS IN CART";
            }
        }
        else
        {
            Response.Cookies["xcartqty"].Value = "0";
            string x = Request.Cookies["xcartqty"].Value;

            if (x == "0")
            {
                xcart.InnerText = "ONLINE MENU";
            }
            else
            {
                xcart.InnerText = x + " ITEMS IN CART";
            }
        }



        if (Session["xuser"] != null)
        {
            string c = (string)Session["xusername"];

            if (c.Length > 15)
            {
                datax.InnerText = "Hi," + c.Substring(0, 15);
            }
            else
            {
                datax.InnerText = "Hi," + c;
            }
        }
        else
        {
            datax.InnerText = "Login";
        }
    }
}