using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class StoreOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ExportBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            var cn = new SqlConnection(Program.Connection);

            DataTable dt = new DataTable();
            dt.Columns.Add("Order No");
            dt.Columns.Add("Order Date");
            dt.Columns.Add("Status");
            dt.Columns.Add("Items");
            dt.Columns.Add("User");
            dt.Columns.Add("Contact");
            dt.Columns.Add("Delivery Type");
            dt.Columns.Add("Delivery Date");
            dt.Columns.Add("Delivery Time");
            dt.Columns.Add("Total Items");
            dt.Columns.Add("Net Total");
            dt.Columns.Add("Pay Mode");
            dt.Columns.Add("Outlet");
            dt.Columns.Add("Address");
            dt.Columns.Add("Source");
            DataRow drRow = null;
            cn.Open();
            var cmd1 = new SqlCommand("SELECT  sm.*, SUBSTRING((SELECT '|' +  'Menu: ' + [Menu Name] + ' Quantity: ' + CAST(CAST([Quantity] AS DECIMAL(20)) AS VARCHAR(20))+ ' Weight: ' + [Weight] + ' Customer Flavour: ' + [Customer Flavour] FROM [XSales Slave] WHERE [Order No] = sm.[Order No] FOR XML PATH ('')), 2, 1000) AS Items FROM[XSales Master] AS sm ORDER BY sm.[ID] DESC", cn);
            var allOrders = cmd1.ExecuteReader();
            if (allOrders.HasRows)
            {
                while (allOrders.Read())
                {
                    var contactNumber = "";
                    var address = allOrders[19].ToString().Split(',');
                    if (address.Length > 0)
                    {
                        var lastString = address[address.Length - 1].Split(':');
                        if (lastString.Length > 1 && lastString[0].Trim() == "DELIVERYCONTACT")
                        {
                            contactNumber = lastString[1].Trim();
                        }
                    }
                    var displayAddress = "";
                    if (allOrders[16].ToString() == "HOME DELIVERY")
                    {
                        displayAddress = allOrders[19].ToString();
                    }

                    drRow = dt.NewRow();
                    drRow[0] = allOrders[1].ToString();
                    drRow[1] = allOrders[2].ToString();
                    drRow[2] = allOrders[20].ToString();
                    drRow[3] = allOrders[25].ToString();
                    drRow[4] = allOrders[15].ToString();
                    drRow[5] = "\"" + contactNumber + "\"";
                    drRow[6] = allOrders[16].ToString();
                    drRow[7] = allOrders[17].ToString();
                    drRow[8] = allOrders[22].ToString();
                    drRow[9] = allOrders[4].ToString();
                    drRow[10] = allOrders[12].ToString();
                    drRow[11] = allOrders[13].ToString();
                    drRow[12] = allOrders[18].ToString();
                    drRow[13] = "\"" + displayAddress + "\"";
                    drRow[14] = allOrders[24].ToString();
                    dt.Rows.Add(drRow);
                }
            }
            cn.Close();

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in dt.Columns)
            {
                sb.Append(string.Format("{0},", col.ColumnName));
            }
            sb.AppendLine();
            foreach (DataRow row in dt.Rows)
            {
                StringBuilder sbn = new StringBuilder();
                sb.Append(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14]));
                sb.AppendLine();
            }

            byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());

            if (bytes != null)
            {
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Length", bytes.Length.ToString());
                Response.AddHeader("Content-disposition", "attachment; filename=\"OrderList_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".csv" + "\"");
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