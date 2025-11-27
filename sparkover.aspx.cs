using System;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["xtran"] = Randompin.Generate(20);
        Session["xcartqty"] = "0";
    }
}