using System;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["xtran"] == null || Session["xtran"] == "")
        {
            Session["xtran"] = Randompin.Generate(20);

            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
            Response.Redirect("online.aspx", true);
            // xcart.InnerText = "ONLINE MENU";
        }
        
    }
}