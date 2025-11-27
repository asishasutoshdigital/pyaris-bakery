using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadServiceReport();
    }

    public void LoadServiceReport()
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT [Status], Count(*) FROM [nodepoint].[dbo].[XvaDownloaded] GROUP BY [Status]", cn);
            var statusList = cmd.ExecuteReader();
            data += "<div class=\"col-md-12 table-responsive OrdersTableContainer\"><table id=\"OrdersTable\" class=\"table table-condensed tableContainer\"><thead><tr><th>Status</th><th>Count</th></tr></thead><tbody>";
            if (statusList.HasRows)
            {
                while (statusList.Read())
                {
                    data += "<tr><td>" + statusList[0].ToString() + "</td><td>" + statusList[1].ToString() + "</td></tr>";
                    if (statusList[0].ToString() == "NEW")
                    {
                        NewCount.InnerText = statusList[1].ToString();
                    }
                }
            }
            data += "</tbody></table></div>";
            cn.Close();
            ServiceReportContainer.InnerHtml = data;
        }
        catch (Exception ex)
        {
        }
    }
}