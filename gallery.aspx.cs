using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gallery : System.Web.UI.Page
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