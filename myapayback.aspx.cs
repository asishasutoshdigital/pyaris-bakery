using System;
using System.Data.SqlClient;


public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xuser"] != null)
        {

            LoadOrders();
        }
        else
        {
            Response.Redirect("login.aspx?mya=a");
        }


    }

    public void LoadOrders()
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();

            var cmd = new SqlCommand("select  coalesce(sum([Payback Points]),0) from [Payback_Details] where [Mobile No]='" + (string)Session["xuser"] + "'", cn);

            var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                data = dr[0].ToString();
                point.InnerText = dr[0].ToString();
            }

            string ty = "";

            if (data != "" && data != "0")
            {
                var cmdx =
                           new SqlCommand(
                 "select  [Payback Points],[Date],[Order No],[Type],[Location] from [Payback_Details] where [Mobile No]='" +
                (string)Session["xuser"] + "' order by [id] desc", cn);

                var drx = cmdx.ExecuteReader();
                ty = " <table class=\"table table-condensed table-responsive tableContainer\"><thead><tr><th class =\"col-md-2\"> Points </th> <th class =\"col-md-2\"> Date </th> <th class =\"col-md-3\"> Description / Order </th><th class =\"col-md-2\"> Type </th> <th class =\"col-md-2\"> Location </th></tr></thead></tbody>";

                while (drx.Read())
                {
                    ty += "<tr> <td class=\"col-md-2\"> " + drx[0].ToString() + " </td> <td class=\"col-md-2\"> " + drx[1].ToString() + "  </td>  <td class=\"col-md-3\"> " + drx[2].ToString() + "  </td><td class=\"col-md-2\"> " + drx[3].ToString() + "  </td> <td class=\"col-md-2\"> " + drx[4].ToString() + "  </td> </tr>";
                }

                ty += "</tbody></table> ";
            }

            cn.Close();

            xdfg.InnerHtml = ty;
        }
        catch (Exception ex)
        {
            //  WebMsgBox.Show(ex.Message);
        }
    }
}