using System;
using System.Data.SqlClient;

public partial class vpayinit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var transaction = Request.QueryString["tranid"];

            var t = (FetchXData(transaction));

            if (t != "")
            {
                var user = Request.Cookies["yphone"].Value;
                var cn = new SqlConnection(Program.Connection);
                cn.Open();
                var cmd = new SqlCommand("select * from [xUser Details] where [Mobile No]='" + user + "'", cn);
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        Session["xuser"] = dr[1].ToString();
                        Session["xusername"] = dr[2].ToString();
                    }
                }
                cn.Close();

                Response.Clear();
                Response.Status = "302 Object Moved";
                Response.RedirectLocation = "sparkover.aspx";
                Response.End();
            }
            else
            {
                Response.Redirect("sparkfail.aspx");
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.ToString());
            //Response.Write(ex.ToString());
        }
    }

    public string FetchXData(string transaction)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            string data = "";
            cn.Open();
            var cmd =
                new SqlCommand(
                    "select [id] from [PG Provider Instamojo] where  [Transaction]='" + transaction + "'  and [Payment Status]='SUCCESS'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    data = dr[0].ToString();
                }
            }
            cn.Close();
            return data;
        }
        catch (Exception ex)
        {
            return "-1";
        }
    }
}