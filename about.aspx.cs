using System;

public partial class about : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["xcartqty"] != null)
        {
            string x = Request.Cookies["xcartqty"].Value;

        }
        else
        {
            Response.Cookies["xcartqty"].Value = "0";
            string x = Request.Cookies["xcartqty"].Value;

        }
    }
}