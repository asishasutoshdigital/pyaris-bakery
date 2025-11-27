using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class Customers : System.Web.UI.Page
{
    public List<CustomersModel> CustomersList;
    protected void Page_Load(object sender, EventArgs e)
    {
            LoadCustomers(YearDropDown.SelectedValue);
    }

    public void LoadCustomers(string months)
    {
        string data = "";
        CustomersList = new List<CustomersModel>();
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT DISTINCT ud.[ID], [Mobile No], [Name], [Email], [Address1], [Address2], [City] ,[PinCode] FROM [nodepoint].[dbo].[XUser Details] AS ud INNER JOIN [nodepoint].[dbo].[XSales Master] AS sm ON sm.[User] = ud.[Mobile No] WHERE CONVERT(DATETIME, [Date] + ' ' + [Time], 103) >= DATEADD(MONTH, -" + months + ", GETDATE()) AND sm.[Status] <> 'CANCELLED'", cn);
            var purchasedCustomers = cmd.ExecuteReader();
            data += "<div class=\"col-md-12 table-responsive OrdersTableContainer\"><table id=\"OrdersTable\" class=\"table table-condensed tableContainer\"><thead><tr><th>User Id</th><th>Mobile Number</th><th>Name</th><th>Email</th><th class=\"Addr-Order-Contiainer\">Address 1</th><th class=\"Addr-Order-Contiainer\">Address 2</th><th>City</th><th>Pincode</th></tr></thead><tbody>";
            if (purchasedCustomers.HasRows)
            {
                while (purchasedCustomers.Read())
                {
                    var customer = new CustomersModel()
                    {
                        UserId = purchasedCustomers[0].ToString(),
                        MobileNumber = purchasedCustomers[1].ToString(),
                        Name = purchasedCustomers[2].ToString(),
                        Email = purchasedCustomers[3].ToString(),
                        Address1 = purchasedCustomers[4].ToString(),
                        Address2 = purchasedCustomers[5].ToString(),
                        City = purchasedCustomers[6].ToString(),
                        Pincode = purchasedCustomers[7].ToString()
                    };
                    CustomersList.Add(customer);
                    data += "<tr><td>" + purchasedCustomers[0].ToString() + "</td><td>" + purchasedCustomers[1].ToString() + "</td><td>" + purchasedCustomers[2].ToString() + "</td><td>" + purchasedCustomers[3].ToString() + "</td><td class=\"Addr-Order-Contiainer\">" + purchasedCustomers[4].ToString() + "</td><td class=\"Addr-Order-Contiainer\">" + purchasedCustomers[5].ToString() + "</td><td>" + purchasedCustomers[6].ToString() + "</td><td>" + purchasedCustomers[7].ToString() + "</td></tr>";
                }
            }
            data += "</tbody></table></div>";
            cn.Close();
            CustomersTableContainer.InnerHtml = data;
        }
        catch (Exception ex)
        {
        }
    }

    protected void ExportBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("User Id");
            dt.Columns.Add("Mobile Number");
            dt.Columns.Add("Name");
            dt.Columns.Add("Email");
            dt.Columns.Add("Address 1");
            dt.Columns.Add("Address 2");
            dt.Columns.Add("City");
            dt.Columns.Add("Pincode");

            DataRow drRow = null;
            foreach (var customer in CustomersList)
            {
                drRow = dt.NewRow();
                drRow[0] = customer.UserId;
                drRow[1] = customer.MobileNumber;
                drRow[2] = customer.Name;
                drRow[3] = customer.Email;
                drRow[4] = "\"" + customer.Address1 + "\"";
                drRow[5] = "\"" + customer.Address2 + "\"";
                drRow[6] = "\"" + customer.City + "\"";
                drRow[7] = customer.Pincode;
                dt.Rows.Add(drRow);
            }

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in dt.Columns)
            {
                sb.Append(string.Format("{0},", col.ColumnName));
            }
            sb.AppendLine();
            foreach (DataRow row in dt.Rows)
            {
                StringBuilder sbn = new StringBuilder();
                sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]));
                sb.AppendLine();
            }

            byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());

            if (bytes != null)
            {
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Length", bytes.Length.ToString());
                Response.AddHeader("Content-disposition", "attachment; filename=\"CustomersList_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".csv" + "\"");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {

        }
    }
}