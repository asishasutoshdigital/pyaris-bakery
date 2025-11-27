using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xtran"] == null || Session["xtran"] == "")
        {
            Session["xtran"] = Randompin.Generate(20);

            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
        }
        try
        {
            if (!Page.IsPostBack)
            {
                /* */
                var dsa = Request.QueryString["isFlavour"];
                LoadForSubGroup(Request.QueryString["xdt"], Request.QueryString["grp"], Convert.ToBoolean(Request.QueryString["isFlavour"]));
            }


        }
        catch (Exception)
        {

        }
    }

    public void LoadForSubGroup(string subgroup, string grp, bool isFlavour = false)
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        List<ProductsModel> productsList = new List<ProductsModel>();
        //try
        //{
        //    cn.Open();
        //    if (isFlavour)
        //    {
        //        var cmd1 = new SqlCommand("SELECT DISTINCT [ID], [Menu Name], [Sell Price], [Group], [Sub Group], [Active] FROM [XMaster Menu] WHERE [Flavour] = '" + subgroup.Replace("'", "") + "'", cn);
        //        var dr1 = cmd1.ExecuteReader();
        //        if (dr1.HasRows)
        //        {
        //            while (dr1.Read())
        //            {
        //                var cmd2 = new SqlCommand("SELECT [ID] FROM [XMaster Menu] WHERE [Menu Name] = '" + dr1[1].ToString() + "' AND [Sub Group] = '" + dr1[4].ToString() + "' ORDER BY [ID]", cn);
        //                var dr2 = cmd2.ExecuteReader();
        //                if (dr2.Read())
        //                {
        //                    var product = new ProductsModel()
        //                    {
        //                        Id = dr1[0].ToString(),
        //                        MenuName = dr1[1].ToString(),
        //                        SellPrice = dr1[2].ToString(),
        //                        Group = dr1[3].ToString(),
        //                        Active = dr1[5].ToString(),
        //                        Feature2 = dr2[0].ToString()
        //                    };
        //                    productsList.Add(product);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //comd = new SqlCommand("SELECT DISTINCT [Menu Name] FROM [XMaster Menu] WHERE [Feature 1] LIKE '%" + subgroup.Replace("'", "") + "%'", cn);
        //        var cmdx = new SqlCommand("SELECT distinct [Menu Name] FROM [XMaster Menu] where [Sub group]='" + subgroup.Replace("'", "") + "'", cn);
        //        var drx = cmdx.ExecuteReader();
        //        if (drx.HasRows)
        //        {
        //            while (drx.Read())
        //            {
        //                var cmd = new SqlCommand("select [id],[menu name],[sell price],[Group],[active] from [XMaster Menu] where [menu name]='" + drx[0].ToString() + "' order by [id]", cn);
        //                var dr = cmd.ExecuteReader();
        //                if (dr.HasRows)
        //                {
        //                    if (dr.Read())
        //                    {
        //                        var active = dr[4].ToString();
        //                        if (dr[3].ToString() == "CAKES")
        //                        {
        //                            var cmdx1 = new SqlCommand("SELECT [Flavour] FROM [XMaster Menu] where [Menu Name]='" + dr[1].ToString() + "' AND active='1'", cn);
        //                            var drx1 = cmdx1.ExecuteReader();
        //                            if (drx1.HasRows)
        //                            {
        //                                active = "1";
        //                            }
        //                            else
        //                            {
        //                                active = "0";
        //                            }
        //                        }

        //                        var product = new ProductsModel()
        //                        {
        //                            Id = dr[0].ToString(),
        //                            MenuName = dr[1].ToString(),
        //                            SellPrice = dr[2].ToString(),
        //                            Group = dr[3].ToString(),
        //                            Active = active,
        //                            Feature2 = dr[0].ToString()
        //                        };
        //                        productsList.Add(product);
        //                    }
        //                }
        //            }
        //        }
        //    }


        try
        {
            cn.Open();
            var cmdx = new SqlCommand();
            if (grp != null)
            {
                cmdx = new SqlCommand("SELECT [id],[menu name],[sell price],[Group],[active] FROM [XMaster Menu] WHERE [Group]='" + grp + "' AND [Sub group] LIKE '%" + subgroup.Replace("'", "") + "%' AND [active] = 1", cn);
            }
            else
            {
                cmdx = new SqlCommand("SELECT [id],[menu name],[sell price],[Group],[active] FROM [XMaster Menu] WHERE [Sub group] LIKE '%" + subgroup.Replace("'", "") + "%' AND [active] = 1", cn);
            }
            var drx = cmdx.ExecuteReader();
            if (drx.HasRows)
            {
                while (drx.Read())
                {
                    if (drx[3].ToString() != "CAKES")
                    {
                        var product = new ProductsModel()
                        {
                            Id = drx[0].ToString(),
                            MenuName = drx[1].ToString(),
                            SellPrice = drx[2].ToString(),
                            Group = drx[3].ToString(),
                            Active = drx[4].ToString(),
                        };
                        productsList.Add(product);
                    }
                }
            }

            if (productsList.Count > 0)
            {
                Random rand = new Random(); // Initialize outside loop to avoid repeating same value

                foreach (var product in productsList)
                {
                    var imagePath = "images/svg-icons/img-placeholder.svg";
                    string[] filelist = Directory.GetFiles(Server.MapPath(Program.ImagePath), product.MenuName + "*.jpg");
                    if (filelist.Length > 0)
                    {
                        imagePath = string.Concat(Program.ImagePath, Path.GetFileName(filelist[0]));
                    }

                    int ratingInt = rand.Next(4, 5); // Base: 4 
                    int ratingDecimal = rand.Next(0, 10); // Decimal: 0–9

                    double actualRating = double.Parse(ratingInt + "." + ratingDecimal);

                    string ratingHTML =
                        "<div class='rating-box'>" +
                            "<div><span class='star'> ★</span> <span class='rating-text'>" + actualRating.ToString("0.0") + "</span></div>" +
                        "</div>";


                    if (product.Active == "0")
                    {
                        data += "<a class=\"list-productcard\" href=\"sparkdetails.aspx?id=" + product.Id + "&isFlavour=" + isFlavour + "\">" +
                                "<div class=\"productcard-image\"><img class=\"lazy\" src=\"" + imagePath + "\" alt=\"" + product.MenuName + " - Paris bakery\" class=\"product-image\" width=\"100%\" height=\"100%\"></div>" +
                                "<div class='veg-symbol'></div>" +
                                "<div class=\"out-of-stock-text product-card-title\">OUT OF STOCK</div>" +
                                "<div class=\"product-card-title\">" + product.MenuName + "</div>" +
                                "<div class=\"product-card-price\"><img src=\"images/svg-icons/rupees.svg\" alt=\"Rs\" width=\"7.5px\" height=\"11.2px\" /> " + product.SellPrice + "</div>" +
                                "</a>";
                    }
                    else
                    {
                        data += "<a class=\"list-productcard\" href=\"sparkdetails.aspx?id=" + product.Id + "&isFlavour=" + isFlavour + "\">" +
                                "<div class=\"productcard-image\"><img class=\"lazy\" src=\"" + imagePath + "\" alt=\"" + product.MenuName + " - Paris bakery\" class=\"product-image\" width=\"100%\" height=\"100%\"></div>" +
                                ratingHTML +
                                "<div class='veg-symbol'></div>" +
                                "<div class=\"product-card-title\">" + product.MenuName + "</div>" +
                                "<div class=\"product-card-price\"><img src=\"images/svg-icons/rupees.svg\" alt=\"Rs\" width=\"7.5px\" height=\"11.2px\" /> " + product.SellPrice + "</div>" +
                                "</a>";
                    }
                }
            }
            else
            {
                data = "<h3>NO ITEMS FOUND )-:</h3>";
            }


            cn.Close();

            xfdata.InnerHtml = data;
            xdisplayheader.InnerText = subgroup;
            Program.SelectedSubGroup = subgroup;

            // cartqty.InnerText= Request.Cookies["xcartqty"].Value;

        }
        catch (Exception ex)
        {
            Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
        }
    }
}