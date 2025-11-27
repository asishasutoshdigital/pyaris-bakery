using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public partial class Products : System.Web.UI.Page
{
    public List<ProductsModel> ProductsList;
    bool isPageRefreshed = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadProducts();

        if (!IsPostBack)
        {
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
            Session["SessionId"] = ViewState["ViewStateId"].ToString();

        }
        else
        {
            if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
            {
                isPageRefreshed = true;
            }

            Session["SessionId"] = System.Guid.NewGuid().ToString();
            ViewState["ViewStateId"] = Session["SessionId"].ToString();
        }
    }

    public void LoadProducts()
    {
        string data = "";
        ProductsList = new List<ProductsModel>();
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT * FROM [XMaster Menu] order by [Id] DESC", cn);
            var allProducts = cmd.ExecuteReader();
            data += "<div class=\"col-md-12 table-responsive OrdersTableContainer\"><table id=\"OrdersTable\" class=\"table table-condensed tableContainer\"><thead><tr><th>Product Id</th><th>Menu Name</th><th>Barcode</th><th>Group</th><th>Sub Group</th><th>Feature 1</th><th>Feature 2</th><th>Feature 3</th><th>Feature 4</th><th>Cost Price</th><th>Sell Price</th><th>Tax %</th><th>Stock</th><th>Discount Type</th><th>Discount Value</th><th>Min Weight</th><th>Flavour</th><th>Active</th><th>Modified Date</th></tr></thead><tbody>";
            if (allProducts.HasRows)
            {
                while (allProducts.Read())
                {
                    var product = new ProductsModel()
                    {
                        Id = allProducts[0].ToString(),
                        MenuName = allProducts[1].ToString(),
                        Barcode = allProducts[2].ToString(),
                        Group = allProducts[3].ToString(),
                        SubGroup = allProducts[4].ToString(),
                        Feature1 = allProducts[5].ToString(),
                        Feature2 = allProducts[6].ToString(),
                        Feature3 = allProducts[7].ToString(),
                        Feature4 = allProducts[8].ToString(),
                        CostPrice = allProducts[9].ToString(),
                        SellPrice = allProducts[10].ToString(),
                        Tax = allProducts[11].ToString(),
                        Stock = allProducts[12].ToString(),
                        DiscountType = allProducts[13].ToString(),
                        DiscountValue = allProducts[14].ToString(),
                        MinWeight = allProducts[15].ToString(),
                        Flavour = allProducts[16].ToString(),
                        Active = allProducts[17].ToString(),
                        ModifiedDate = allProducts[18].ToString(),
                    };
                    ProductsList.Add(product);
                    data += "<tr><td><a href=\"EditProducts.aspx?productId=" + allProducts[0].ToString() + "\">" + allProducts[0].ToString() + "</a></td><td>" + allProducts[1].ToString() + "</td><td>" + allProducts[2].ToString() + "</td><td>" + allProducts[3].ToString() + "</td><td>" + allProducts[4].ToString() + "</td><td>" + allProducts[5].ToString() + "</td><td>" + allProducts[6].ToString() + "</td><td>" + allProducts[7].ToString() + "</td><td>" + allProducts[8].ToString() + "</td><td>" + allProducts[9].ToString() + "</td><td>" + allProducts[10].ToString() + "</td><td>" + allProducts[11].ToString() + "</td><td>" + allProducts[12].ToString() + "</td><td>" + allProducts[13].ToString() + "</td><td>" + allProducts[14].ToString() + "</td><td>" + allProducts[15].ToString() + "</td><td>" + allProducts[16].ToString() + "</td><td>" + allProducts[17].ToString() + "</td><td>" + allProducts[18].ToString() + "</td></tr>";
                }
            }
            data += "</tbody></table></div>";
            cn.Close();
            AllProducts.InnerHtml = data;
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product load: " + ex.Message, ex);
        }
    }

    protected void ImportBtn_Clicked(object sender, EventArgs e)
    {
        if (!isPageRefreshed)
        {
            try
            {
                if (FileUploadBtn.HasFile)
                {
                    var fileName = FileUploadBtn.FileName.Split('.');
                    bool isCsv = fileName[fileName.Length - 1] == "csv" ? true : false;
                    if (isCsv)
                    {
                        var fileContent = "";
                        fileContent = Encoding.UTF8.GetString(FileUploadBtn.FileBytes, 0, FileUploadBtn.FileBytes.Length);
                        var rows = fileContent.Split('\n');
                        var list = new List<string>();
                        list.AddRange(rows);
                        list.RemoveAt(0);
                        var cn = new SqlConnection(Program.Connection);
                        cn.Open();
                        foreach (var row in list)
                        {
                            Regex regex = new Regex(@"\|");
                            var values = regex.Split(row);
                            if (values.Length == 18)
                            {
                                var isPresent = ProductsList.Any(x => x.MenuName == values[0]);
                                if (isPresent)
                                {
                                    var cmd1 = new SqlCommand("UPDATE [XMaster Menu] SET [Barcode] = '" + values[1] + "' , [Group] = '" + values[2] + "' , [Sub Group] = '" + values[3] + "' , [Feature 1] = '" + values[4].Trim('"') + "' , [Feature 2] = '" + values[5].Trim('"') + "' , [Feature 3] = '" + values[6].Trim('"') + "' , [Feature 4] = '" + values[7].Replace("'", "''").Trim('"') + "' , [Cost Price] = '" + values[8] + "' , [Sell Price] = '" + values[9] + "' , [Tax %] = '" + values[10] + "' , [Stock] = '" + values[11] + "' , [Discount Type] = '" + values[12] + "' , [Discount Value] = '" + values[13] + "' , [Min Weight] = '" + values[14] + "' , [Flavour] = '" + values[15] + "' , [active] = '" + values[16] + "' , [mod_date] = '" + DateTime.Now.ToString() + "' where [Menu Name]='" + values[0] + "'", cn);
                                    cmd1.ExecuteNonQuery();
                                }
                                else
                                {
                                    var cmd2 = new SqlCommand("INSERT INTO [XMaster Menu] VALUES('" + values[0] + "','" + values[1] + "','" + values[2] + "','" + values[3] + "','" + values[4].Trim('"') + "','" + values[5].Trim('"') + "','" + values[6].Trim('"') + "','" + values[7].Replace("'", "''").Trim('"') + "','" + values[8] + "','" + values[9] + "','" + values[10] + "','" + values[11] + "','" + values[12] + "','" + values[13] + "','" + values[14] + "','" + values[15] + "','" + values[16] + "','" + DateTime.Now.ToString() + "')", cn);
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                        }
                        cn.Close();
                        FileUploadBtn.PostedFile.InputStream.Dispose();
                        WebMsgBox.Show("File Date uploaded");
                        LoadProducts();
                    }
                    else
                    {
                        WebMsgBox.Show("Please Select a CSV File to upload");
                    }
                }
                else
                {
                    WebMsgBox.Show("Please Select a File to upload");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Issue with Product File Upload: " + ex.Message, ex);
            }
        }
    }

    protected void ExportBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            var cn = new SqlConnection(Program.Connection);

            DataTable dt = new DataTable();
            dt.Columns.Add("Menu Name");
            dt.Columns.Add("Barcode");
            dt.Columns.Add("Group");
            dt.Columns.Add("Sub Group");
            dt.Columns.Add("Feature 1");
            dt.Columns.Add("Feature 2");
            dt.Columns.Add("Feature 3");
            dt.Columns.Add("Feature 4");
            dt.Columns.Add("Cost Price");
            dt.Columns.Add("Sell Price");
            dt.Columns.Add("Tax %");
            dt.Columns.Add("Stock");
            dt.Columns.Add("Discount Type");
            dt.Columns.Add("Discount Value");
            dt.Columns.Add("Min Weight");
            dt.Columns.Add("Flavour");
            dt.Columns.Add("Active");
            dt.Columns.Add("Modified Date");

            DataRow drRow = null;
            cn.Open();
            var cmd1 = new SqlCommand("SELECT [Menu Name],[Barcode],[Group],[Sub Group],[Feature 1],[Feature 2],[Feature 3],[Feature 4],[Cost Price],[Sell Price],[Tax %],[Stock],[Discount Type],[Discount Value],[Min Weight],[Flavour],[active],[mod_date] FROM [dbo].[XMaster Menu] order by [Group], [Sub group]", cn);
            var allProducts = cmd1.ExecuteReader();
            if (allProducts.HasRows)
            {
                while (allProducts.Read())
                {
                    drRow = dt.NewRow();
                    drRow[0] = allProducts[0].ToString();
                    drRow[1] = allProducts[1].ToString();
                    drRow[2] = allProducts[2].ToString();
                    drRow[3] = allProducts[3].ToString();
                    drRow[4] = "\"" + allProducts[4].ToString() + "\"";
                    drRow[5] = "\"" + allProducts[5].ToString() + "\"";
                    drRow[6] = "\"" + allProducts[6].ToString() + "\"";
                    drRow[7] = "\"" + allProducts[7].ToString() + "\"";
                    drRow[8] = allProducts[8].ToString();
                    drRow[9] = allProducts[9].ToString();
                    drRow[10] = allProducts[10].ToString();
                    drRow[11] = allProducts[11].ToString();
                    drRow[12] = allProducts[12].ToString();
                    drRow[13] = allProducts[13].ToString();
                    drRow[14] = allProducts[14].ToString();
                    drRow[15] = allProducts[15].ToString();
                    drRow[16] = allProducts[16].ToString();
                    drRow[17] = allProducts[17].ToString().Trim('\r');
                    dt.Rows.Add(drRow);
                }
            }
            cn.Close();

            StringBuilder sb = new StringBuilder();

            foreach (DataColumn col in dt.Columns)
            {
                sb.Append(string.Format("{0}|", col.ColumnName));
            }
            sb.AppendLine();
            foreach (DataRow row in dt.Rows)
            {
                StringBuilder sbn = new StringBuilder();
                sb.Append(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}", row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17]));
                sb.AppendLine();
            }

            byte[] bytes = Encoding.ASCII.GetBytes(sb.ToString());

            if (bytes != null)
            {
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Length", bytes.Length.ToString());
                Response.AddHeader("Content-disposition", "attachment; filename=\"ProductsList_" + DateTime.Now.Date.ToString("dd-MM-yyyy") + ".csv" + "\"");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product File Export: " + ex.Message, ex);
        }
    }
}