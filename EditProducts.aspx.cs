using System;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Web;
using System.Web.UI;

public partial class EditProducts : System.Web.UI.Page
{
    public string productId;
    protected void Page_Load(object sender, EventArgs e)
    {
        productId = Request.QueryString["productId"];
        if (!Page.IsPostBack)
        {
            LoadProduct();
            DisplayProductImage();
        }
    }

    public void LoadProduct()
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT * FROM [XMaster Menu] WHERE [ID] = '" + productId + "'", cn);
            var product = cmd.ExecuteReader();
            if (product.HasRows)
            {
                if (product.Read())
                {
                    ProductId.Value = product[0].ToString();
                    MenuName.Value = ProductTitle.InnerHtml = product[1].ToString();
                    Barcode.Value = product[2].ToString();
                    Group.Value = product[3].ToString();
                    SubGroup.Value = product[4].ToString();
                    Feature1.Value = product[5].ToString();
                    Feature2.Value = product[6].ToString();
                    Feature3.Value = product[7].ToString();
                    Feature4.Value = product[8].ToString();
                    CostPrice.Value = product[9].ToString();
                    SellPrice.Value = product[10].ToString();
                    Tax.Value = product[11].ToString();
                    Stock.Value = product[12].ToString();
                    DiscountType.Value = product[13].ToString();
                    DiscountValue.Value = product[14].ToString();
                    MinWeight.Value = product[15].ToString();
                    Flavour.Value = product[16].ToString();
                    Active.Value = product[17].ToString();
                }
            }
            cn.Close();
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product load : " + ex.Message, ex);

        }
    }

    protected void SaveBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            var cn = new SqlConnection(Program.Connection);
            cn.Open();
            var cmd1 = new SqlCommand("UPDATE [XMaster Menu] SET [Menu Name] = '" + MenuName.Value.Replace("'", "''") + "' , [Barcode] = '" + Barcode.Value.Replace("'", "''") + "' , [Group] = '" + Group.Value.Replace("'", "''") + "' , [Sub Group] = '" + SubGroup.Value.Replace("'", "''")
                + "' , [Feature 1] = '" + Feature1.Value.Replace("'", "''") + "' , [Feature 2] = '" + Feature2.Value.Replace("'", "''") + "' , [Feature 3] = '" + Feature3.Value.Replace("'", "''") + "' , [Feature 4] = '" + Feature4.Value.Replace("'", "''")
                + "' , [Cost Price] = '" + CostPrice.Value + "' , [Sell Price] = '" + SellPrice.Value + "' , [Tax %] = '" + Tax.Value + "' , [Stock] = '" + Stock.Value + "' , [Discount Type] = '" + DiscountType.Value.Replace("'", "''")
                + "' , [Discount Value] = '" + DiscountValue.Value + "' , [Min Weight] = '" + MinWeight.Value + "' , [Flavour] = '" + Flavour.Value.Replace("'", "''") + "' , [active] = '" + Active.Value + "' , [mod_date] = '" + DateTime.Now.ToString()
                + "' where [ID]='" + productId + "'", cn);
            cmd1.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product save : " + ex.Message, ex);
        }
    }

    protected void CloseBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Products.aspx");
        }
        catch(ThreadAbortException)
        {

        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product close : " + ex.Message, ex);
        }
    }

    public void DeleteFileImage(string file_name)
    {
        try
        {
            string path = Server.MapPath(Program.ImagePath + file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();

            }
            DisplayProductImage();
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product Delete : " + ex.Message, ex);
        }
    }
    public class Image_Data
    {
        public string File_Name { get; set; }
        public string Next_Value { get; set; }
    }
    public void DisplayProductImage()
    {
        string data = "";
        var imagePath = "images/svg-icons/img-placeholder.svg";
        string[] filelist = Directory.GetFiles(Server.MapPath(Program.ImagePath), MenuName.Value + "*.jpg");
        try
        {
            if (filelist.Length > 0)
            {
                foreach (var file in filelist)
                {
                    imagePath = string.Concat(Program.ImagePath, Path.GetFileName(file));
                    data += "<div class=\"list-productcard\"> <div class=\"productcard-image\"><img class=\"lazy\" src=\"" + imagePath + "\" alt=\"" + MenuName.Value.ToString() + "\" class=\"product-image\" width=\"100%\" height=\"100%\"></div><div class=\"product-card-title\"> <div class =\"col-sm-6\"><button type=\"button\" class =\"ExportBtn\"  name=\"deletebtn\" onclick=\"DeleteImage($(this).val())\" value=\"" + imagePath + "\">Delete</button> </div> <div class =\"col-sm-6\"><input type=\"file\" accept=\".jpg\"  runat =\"server\"  id=\"uploadbtn\" style=\"display:none\" ></input> <button type=\"button\" class =\"ExportBtn\" id=\"fileupload\" value=\"" + imagePath + "\"  onclick=\"callImport($(this).val())\">Change photo</button> </div> </div> </div>";
                }
            }
            else
            {
                data = "<h3>NO ITEMS FOUND )-:</h3>";

            }
            xfdata.InnerHtml = data;
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product Display : " + ex.Message, ex);
        }
    }
    protected void Exportbtn_Clicked(object sender, EventArgs e)
    {
        try
        {

            string[] filelist = Directory.GetFiles(Server.MapPath(Program.ImagePath), MenuName.Value + "*.jpg");
            if (filelist.Length > 0)
            {


                using (var memoryStream = new MemoryStream())
                {
                    using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        foreach (var file in filelist)
                        {

                            zipArchive.CreateEntryFromFile(file, Path.GetFileName(file));
                        }

                    }
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/zip";
                    HttpContext.Current.Response.AddHeader("Content-Length", memoryStream.Length.ToString());
                    HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=\"" + MenuName.Value.ToString() + "_.zip" + "\"");
                    HttpContext.Current.Response.BinaryWrite(memoryStream.ToArray());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.Close();
                }

            }

            else
            {
                WebMsgBox.Show(string.Concat(MenuName.Value.ToString(), " item do not have image file"));
            }
        }
        catch (Exception ex)
        {
            Log.Error("Issue with Product Image Export : " + ex.Message, ex);
        }
    }

    protected void Getprdimg_Clicked(object sender, EventArgs e)
    {
        DisplayProductImage();
    }
}