using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class ONLINE_sales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtdate1.Text= DateTime.Now.ToString("dd-MM-yyyy");
            cmboutlet.SelectedIndex = 0;
            dv.DataSource = null;
            dv.DataBind();

            string mobile = Request.QueryString["m1"];
            string outlet = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(Request.QueryString["outlet"]));
           if (mobile != "")
            {
                cmboutlet.Items.Clear();
               cmboutlet.Items.Add("Select Outlet");
                string[] store = outlet.Split(',');
                foreach (string str in store)
                {
                    cmboutlet.Items.Add(str);
                }
            }
          
        }
    }

   
    protected void Button2_Click(object sender, EventArgs e)
    {
        var cn = new SqlConnection(Program.Connection2);
        try
        {
            cn.Open();
            if (cmboutlet.Text == "ALL OUTLET")
            {
                var da = new SqlDataAdapter("select [OUTLET],coalesce(round(sum([Total Actual Sale Amount]),0),0) as 'SALE AMOUNT' from [Sales Master] where CONVERT(datetime,[Date],105)>=CONVERT(datetime,'" +
                              txtdate.Text + "',105) and CONVERT(datetime,[Date],105)<=CONVERT(datetime,'" +
                              txtdate1.Text + "',105) and [Paymode]!='ONLINE' GROUP BY [outlet] order by  coalesce(sum([Total Actual Sale Amount]),0) desc", cn);
                DataSet ds = new DataSet();
                ds.Clear(); ds.Reset();
                da.Fill(ds);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];

                var da1 = new SqlDataAdapter("select coalesce(round(sum([Total Actual Sale Amount]),0),0)  from [Sales MAster] where CONVERT(datetime,[Date],105)>=CONVERT(datetime,'" +
                   txtdate.Text +
                   "',105) and CONVERT(datetime,[Date],105)<=CONVERT(datetime,'" +
                  txtdate1.Text + "',105) and [Paymode]!='ONLINE'", cn);
                DataSet ds1 = new DataSet();
                ds1.Clear(); ds1.Reset();
                da1.Fill(ds1);

                dt.Rows.Add("Total :", ds1.Tables[0].Rows[0].ItemArray[0]);

                dv.DataSource = dt;
                dv.DataBind();
            }
            else if (cmboutlet.SelectedIndex != 0)
            {
                var da = new SqlDataAdapter("select [OUTLET],coalesce(round(sum([Total Actual Sale Amount]),0),0) as 'SALE AMOUNT' from [Sales Master] where CONVERT(datetime,[Date],105)>=CONVERT(datetime,'" +
                              txtdate.Text + "',105) and CONVERT(datetime,[Date],105)<=CONVERT(datetime,'" +
                              txtdate1.Text + "',105) and [Paymode]!='ONLINE' AND [Outlet] LIKE '%" + cmboutlet.Text + "%' GROUP BY [outlet] order by  coalesce(sum([Total Actual Sale Amount]),0) desc", cn);
                DataSet ds = new DataSet();
                ds.Clear(); ds.Reset();
                da.Fill(ds);

                dv.DataSource = ds.Tables[0];
                dv.DataBind();
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void dv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            if (e.Row.Cells[0].Text == "Total: ")
            {
                e.Row.ForeColor = System.Drawing.Color.Green;
                e.Row.Font.Bold = true;
            }
        }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        string con = "server = 204.44.72.79,5000; database = NODEPOINT; user = sa; pwd = JaySaiBaba123; multipleactiveresultsets = true; ";
        var cn = new SqlConnection(con);
        try
        {
            cn.Open();
            var da = new SqlDataAdapter("select [OUTLET],coalesce(round(sum([Total Actual Sale Amount]),0),0) as 'SALE AMOUNT' from [Sales Master] where CONVERT(datetime,[Date],105)>=CONVERT(datetime,'" +
                            txtdate.Text + "',105) and CONVERT(datetime,[Date],105)<=CONVERT(datetime,'" +
                            txtdate1.Text + "',105) and [Paymode]!='ONLINE' GROUP BY [outlet] order by  coalesce(sum([Total Actual Sale Amount]),0) desc", cn);
            DataSet ds = new DataSet();
            ds.Clear(); ds.Reset();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            var da1 = new SqlDataAdapter("select coalesce(round(sum([Total Actual Sale Amount]),0),0)  from [Sales MAster] where CONVERT(datetime,[Date],105)>=CONVERT(datetime,'" +
               txtdate.Text +
               "',105) and CONVERT(datetime,[Date],105)<=CONVERT(datetime,'" +
              txtdate1.Text + "',105) and [Paymode]!='ONLINE'", cn);
            DataSet ds1 = new DataSet();
            ds1.Clear(); ds1.Reset();
            da1.Fill(ds1);

            dt.Rows.Add("Total :", ds1.Tables[0].Rows[0].ItemArray[0]);

            dv.DataSource = dt;
            dv.DataBind();
            cn.Close();
        }
        catch(Exception ex)
        {

        }
        }

}