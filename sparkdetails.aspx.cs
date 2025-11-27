using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class spark : System.Web.UI.Page
{
    static string xmenu = "";
    static string xgroup = "";
    static string xsubgroup = "";
    static string xsellprice = "0";
    static string xtaxpercent = "0";
    static string xcostprice = "0";
    static string xminweight = "0";
    static string xcartqty = "0";
    static string selectedFlavour = "";
    static string id = "";
    static bool isFlavour = false;
    public string ItemUrl = "";
    public string ImageUrl = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ImageButton2.Visible = false;
            ImageButton2.Enabled = false;

            ProductThumbnail_1.Visible = false;
            ProductThumbnail_2.Visible = false;
            ProductThumbnail_3.Visible = false;

            var cn = new SqlConnection(Program.Connection);
            try
            {
                id = Request.QueryString["id"];
                isFlavour = Convert.ToBoolean(Request.QueryString["isFlavour"]);

                cn.Open();
                var cmd = new SqlCommand("SELECT DISTINCT [Menu Name],[Group],[Sub Group],[Sell Price],[Tax %],[Cost Price],[Feature 3],[Feature 4],[Min weight],[Flavour],[Active],[Feature 1],[Feature 2] FROM [XMaster Menu] WHERE [ID]='" + id + "'", cn);
                var dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        xmenu = dr[0].ToString();
                        xgroup = dr[1].ToString();
                        xsubgroup = dr[2].ToString();
                        xsellprice = dr[3].ToString();
                        xtaxpercent = dr[4].ToString();
                        xcostprice = dr[5].ToString();
                        xminweight = dr[8].ToString();

                        ItemUrl = Request.Url.AbsoluteUri;

                        DirectoryInfo d = new DirectoryInfo(Server.MapPath("images/productimages/"));
                        var count = 1;
                        StringBuilder imageBuilder = new StringBuilder();
                        foreach (var file in d.GetFiles("*" + xmenu + "*.jpg"))
                        {
                            imageBuilder.Append("<li><div class=\"smallimgBoxImageSize\" onclick=\"smallimgclick(this)\"><img src=\"images/productimages/" + file.Name + "\" alt=\"" + xmenu + "-Thumbnail\" /></div></li>");

                            if (count == 1)
                            {
                                ximgshowser.InnerHtml = @"
                                <div class='imgBox'>
                                    <div class='img-zoom-container'>
                                        <img id='bigimg' src='images/productimages/" + file.Name + @"' alt='" + xmenu + @"' class='imgBoxImageSize' />
                                        <div id='zoomResult'></div>
                                    </div>
                                    <div class='veg-symbol'></div>
                                </div>";
                                ImageUrl = Program.PaymentGatewayURL + "images/productimages/" + file.Name + "";
                            }
                            count++;
                        }
                        prodImages.InnerHtml = imageBuilder.ToString();

                        xmenumane.InnerText = dr[0].ToString();
                        xgroupsubgroup.InnerText = Program.SelectedSubGroup;

                        xrate.InnerHtml = "<meta itemprop=\"availability\" content=\"InStock\"><meta itemprop=\"url\" content=\"" + Request.Url.AbsoluteUri + "\"><span itemprop=\"priceCurrency\" content=\"INR\"><img src=\"images/svg-icons/rupees.svg\" alt=\"Rs\" width=\"15px\" height=\"30px\" /></span><span itemprop=\"price\" content=\"" + dr[3].ToString() + "\"> " + dr[3].ToString() + "</span>";

                        if (dr[1].ToString() == "Cakes")
                        {
                            SetProductWeight(dr[8].ToString());

                            if (dr["Feature 2"].ToString().Trim() != "") { 
                                var flavoursList = dr["Feature 2"].ToString().Split(',');
                                foreach (var flavour in flavoursList)
                                {
                                    int i = 0;
                                    DropDownList1.Items.Add(flavour);
                                    i++;
                                }
                                DropDownList1.SelectedIndex = 0;
                                selectedFlavour = DropDownList1.SelectedItem.Text;
                            }
                            else
                            {
                                x3.Visible = false;
                            }                            
                        }
                        else
                        {
                            x1.Visible = false;
                            x2.Visible = false;
                            x3.Visible = false;
                        }
                        if ((string)Session["xuser"] == Program.Admin_PhoneNumber)
                        {
                            x1.Visible = false;
                            x4.Visible = false;
                            x5.Visible = false;
                        }
                        if (dr["active"].ToString() == "0")
                        {
                            ImageButton1.Visible = false;
                            ImageButton2.Visible = true;
                        }
                    }
                }
                cn.Close();
                if (Session["xcartqty"] != null)
                    xcartqty = Session["xcartqty"].ToString();
                else
                    xcartqty = "0";
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        ximgshowser.InnerHtml = "<img src=\"menupic/big/" + id + "b.png\" class=\"imgBoxImageSize\"/>";
    }
    protected void Weight_Changed(object sender, EventArgs e)
    {
        double selectedWeight = double.Parse(weightradiobtns.SelectedValue);
        double minweight = double.Parse(xminweight);
        double sellprice = double.Parse(xsellprice);

        double onekgprice = Math.Round(sellprice / minweight, 0);
        double selectedWeightprice = Math.Round(onekgprice * selectedWeight, 0);

        xrate.InnerHtml = "<meta itemprop=\"availability\" content=\"InStock\"><meta itemprop=\"url\" content=\"" + Request.Url.AbsoluteUri + "\"><span itemprop=\"priceCurrency\" content=\"INR\"><img src=\"images/svg-icons/rupees.svg\" alt=\"Rs\" width=\"15px\" height=\"30px\" /></span><span itemprop=\"price\" content=\"" + selectedWeightprice.ToString() + "\"> " + selectedWeightprice.ToString() + "</span>";
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (Session["xtran"] != null)
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                //string id = Request.QueryString["id"];
                var quan = xqty.Value != "" ? xqty.Value : "0";
                double qty = double.Parse(quan);
                string wish = xwish.Value;
                string transaction = (string)Session["xtran"];

                string menu = xmenu;
                string group = xgroup;
                string subgroup = Program.SelectedSubGroup;

                double sellprice = double.Parse(xsellprice);
                double costprice = double.Parse(xcostprice);

                double taxp = double.Parse(xtaxpercent);
                double taxamount = Math.Round((taxp / 100) * sellprice, 2);

                double totalcostprice = Math.Round(costprice * qty, 2);
                double totalsaleamount = Math.Round(sellprice * qty, 0);
                double totaltaxamount = Math.Round(taxamount * qty, 2);

                double minweight = double.Parse(xminweight);

                if (qty > 0)
                {
                    if (group == "Cakes")
                    {
                        double weight = double.Parse(weightradiobtns.SelectedValue);

                        if (minweight > weight)
                        {
                            WebMsgBox.Show("Minimum weight must be more than " + minweight + " Kg");
                        }
                        else
                        {

                            double r = sellprice;
                            double onekgprice = sellprice / minweight;

                            sellprice = Math.Round(onekgprice * weight, 0);
                            costprice = Math.Round(onekgprice * weight, 0);

                            taxamount = Math.Round((taxp / 100) * sellprice, 2);
                            totalcostprice = Math.Round(costprice * qty, 2);
                            totalsaleamount = Math.Round(sellprice * qty, 0);
                            totaltaxamount = Math.Round(taxamount * qty, 2);

                            var selectedflav = DropDownList1.SelectedItem != null ? DropDownList1.SelectedItem.Text : "";

                            cn.Open();
                            var cmd =
                                new SqlCommand(
                                    "insert into [XSales Slave] values ('" + transaction + "','" +
                                    DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH:mm") + "','" +
                                    id + "','" + menu + "','" + group + "','" + subgroup + "','','','','','" + wish + "','" +
                                    costprice + "','" + sellprice + "','" + taxp + "','" + taxamount + "','" + qty + "','" +
                                    totalcostprice + "','" + totalsaleamount + "','" + totaltaxamount + "','" + transaction +
                                    "','','" + selectedflav + "','" + weightradiobtns.SelectedValue + "')", cn);
                            cmd.ExecuteNonQuery();
                            cn.Close();

                            int y = int.Parse(xcartqty);
                            y = y + 1;

                            xcartqty = y.ToString();
                            Session["xcartqty"] = xcartqty;
                            Response.Redirect("sparkcart.aspx");
                        }
                        //   WebMsgBox.Show("" + minweight + " Kg   "+weight);
                    }
                    else
                    {
                        cn.Open();

                        var cmd =
                            new SqlCommand(
                                "insert into [XSales Slave] values ('" + transaction + "','" +
                                DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH:mm") + "','" + id +
                                "','" + menu + "','" + group + "','" + subgroup + "','','','','','" + wish + "','" +
                                costprice + "','" + sellprice + "','" + taxp + "','" + taxamount + "','" + qty + "','" +
                                totalcostprice + "','" + totalsaleamount + "','" + totaltaxamount + "','" + transaction +
                                "','','','')", cn);
                        cmd.ExecuteNonQuery();

                        cn.Close();

                        int y = int.Parse(xcartqty);

                        y = y + 1;

                        xcartqty = y.ToString();
                        Session["xcartqty"] = xcartqty;
                        Response.Redirect("sparkcart.aspx");
                    }
                }
                else
                {
                    WebMsgBox.Show("Quantity Must Be 1 or More");
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.Message);
            }
        }
        else
        {
            Session["xtran"] = Randompin.Generate(20);
            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
            Response.Redirect("Default.aspx", true);
        }
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();

            string menu = xmenu;
            selectedFlavour = DropDownList1.SelectedItem.Text;
            var cmd = new SqlCommand("select [min weight],[sell price], [active] from [xmaster menu] where [menu name]='" + menu + "' and [flavour]='" + selectedFlavour + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    SetProductWeight(dr[0].ToString());
                    xsellprice = dr[1].ToString();
                    xminweight = dr[0].ToString();

                    double weight = double.Parse(dr[0].ToString());
                    double rate = double.Parse(dr[1].ToString());
                    double amount = weight * rate;

                    xrate.InnerHtml = "<img src=\"images/svg-icons/rupees.svg\" alt=\"Rs\" width=\"15px\" height=\"30px\" /> " + amount + "  (<img src=\"images/svg-icons/rupees.svg\" alt=\"Rs\" width=\"15px\" height=\"30px\" /> " + dr[1].ToString() + "/Kg)";

                    if (dr[2].ToString() == "0")
                    {
                        ImageButton1.Visible = false;
                        ImageButton2.Visible = true;
                    }
                    else
                    {
                        ImageButton1.Visible = true;
                        ImageButton2.Visible = false;
                    }
                }
            }
            cn.Close();
        }
        catch (Exception ex)
        {
            WebMsgBox.Show(ex.Message);
        }
    }

    void SetProductWeight(string minWeight)
    {
        weightradiobtns.Items.Clear();

        var minimumWeight = Convert.ToDouble(minWeight);
        var weight_1 = (Convert.ToDouble(minWeight) + 0.5).ToString();
        var weight_2 = (Convert.ToDouble(minWeight) + 1).ToString();
        var weight_3 = (Convert.ToDouble(minWeight) + 1.5).ToString();
        var weight_4 = (Convert.ToDouble(minWeight) + 2).ToString();

        var weight_5 = (Convert.ToDouble(minWeight) + 2.5).ToString();
        var weight_6 = (Convert.ToDouble(minWeight) + 3.5).ToString();
        var weight_7 = (Convert.ToDouble(minWeight) + 4.5).ToString();


        weightradiobtns.Items.Add(new ListItem(minWeight + " Kg", minWeight));

        if (minimumWeight == 0.5)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
            weightradiobtns.Items.Add(new ListItem(weight_2 + " Kg", weight_2));
            weightradiobtns.Items.Add(new ListItem(weight_3 + " Kg", weight_3));
            weightradiobtns.Items.Add(new ListItem(weight_4 + " Kg", weight_4));
            weightradiobtns.Items.Add(new ListItem(weight_5 + " Kg", weight_5));
            weightradiobtns.Items.Add(new ListItem(weight_6 + " Kg", weight_6));
            weightradiobtns.Items.Add(new ListItem(weight_7 + " Kg", weight_7));
        }
        else if (minimumWeight == 1)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
            weightradiobtns.Items.Add(new ListItem(weight_2 + " Kg", weight_2));
            weightradiobtns.Items.Add(new ListItem(weight_3 + " Kg", weight_3));
            weightradiobtns.Items.Add(new ListItem(weight_4 + " Kg", weight_4));
            weightradiobtns.Items.Add(new ListItem(weight_5 + " Kg", weight_5));
            weightradiobtns.Items.Add(new ListItem(weight_6 + " Kg", weight_6));
        }
        else if (minimumWeight == 1.5)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
            weightradiobtns.Items.Add(new ListItem(weight_2 + " Kg", weight_2));
            weightradiobtns.Items.Add(new ListItem(weight_3 + " Kg", weight_3));
            weightradiobtns.Items.Add(new ListItem(weight_4 + " Kg", weight_4));
            weightradiobtns.Items.Add(new ListItem(weight_5 + " Kg", weight_5));
        }
        else if (minimumWeight == 2)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
            weightradiobtns.Items.Add(new ListItem(weight_2 + " Kg", weight_2));
            weightradiobtns.Items.Add(new ListItem(weight_3 + " Kg", weight_3));
            weightradiobtns.Items.Add(new ListItem(weight_4 + " Kg", weight_4));
        }
        else if (minimumWeight == 3)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
            weightradiobtns.Items.Add(new ListItem(weight_2 + " Kg", weight_2));
            weightradiobtns.Items.Add(new ListItem(weight_3 + " Kg", weight_3));
        }
        else if (minimumWeight == 4)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
            weightradiobtns.Items.Add(new ListItem(weight_2 + " Kg", weight_2));
        }
        else if (minimumWeight == 5)
        {
            weightradiobtns.Items.Add(new ListItem(weight_1 + " Kg", weight_1));
        }
        weightradiobtns.SelectedIndex = 0;
    }

    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
    }
}