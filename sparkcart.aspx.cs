using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class spark : System.Web.UI.Page
{
    static string xpaybackpoint = "";
    static string xcartqty = "";
    static string xgiftcardpoint = "";
    static string xgiftcarddetails = "";
    static string x = "";
    private string availablePoint = string.Empty;
    private double discountAmount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xtran"] == null || Session["xtran"].ToString() == "")
        {
            Session["xtran"] = Randompin.Generate(20);
            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
        }

        if (!Page.IsPostBack)
        {
            xpaybackpoint = "0";

            if (Session["xcartqty"] != null)
                x = Session["xcartqty"].ToString();

            xgiftcardpoint = "0";
            xgiftcarddetails = "";
            Session["DiscountAmount"] = "0";
            Session["RedeemedGiftCards"] = "";
            Session["DiscountSource"] = "";

            // Handle deletion
            if (!string.IsNullOrEmpty(Request.QueryString["del"]))
            {
                LoadcartandDelete(Request.QueryString["del"]);
                Response.Redirect("sparkcart.aspx"); // Clear query string
                return;
            }

            // Load cart initially
            Loadcart();

            // Handle Payback
            if (Program.isPaybackAvailable)
                LoadPaybackPoint();

            if (availablePoint == "0")
                payBackContainer.Visible = false;

            // Handle quantity increase
            string plus = Request.QueryString["plus"];
            if (!string.IsNullOrEmpty(plus))
            {
                int itemId = int.Parse(plus);
                UpdateQuantityInDatabase(itemId, +1);
                Response.Redirect("sparkcart.aspx"); // Clear query string
                return;
            }

            // Handle quantity decrease
            string minus = Request.QueryString["minus"];
            if (!string.IsNullOrEmpty(minus))
            {
                int itemId = int.Parse(minus);
                int currentQty = GetCurrentQuantity(itemId);
                if (currentQty > 1)
                {
                    UpdateQuantityInDatabase(itemId, -1);
                    Response.Redirect("sparkcart.aspx"); // Clear query string
                    return;
                }
                else if (currentQty == 1)
                {
                    LoadcartandDelete(Request.QueryString["minus"]);
                    Response.Redirect("sparkcart.aspx"); // Clear query string
                    return;
                }
            }
        }
    }

    private int GetCurrentQuantity(int itemId)
    {
        int quantity = 0;

        using (SqlConnection cn = new SqlConnection(Program.Connection))
        {
            try
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [quantity] FROM [XSales Slave] WHERE [id] = @ItemId AND [Transaction] = @Transaction", cn))
                {
                    cmd.Parameters.AddWithValue("@ItemId", itemId);
                    cmd.Parameters.AddWithValue("@Transaction", Session["xtran"].ToString());

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            quantity = Convert.ToInt32(reader["quantity"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        return quantity;
    }

    private void UpdateQuantityInDatabase(int itemId, int change)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("UPDATE [XSales Slave] SET [Quantity] = [Quantity] + @Change, [total sale amount] = ([Quantity] + @Change) * [Sell Price] WHERE [id] = @ItemId AND [Transaction] = '" + (string)Session["xtran"] + "'", cn);
            cmd.Parameters.AddWithValue("@ItemId", itemId);
            cmd.Parameters.AddWithValue("@Change", change);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            cn.Close();
        }
    }
    public void LoadPaybackPoint()
    {
        if (Session["xuser"] != null)
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                cn.Open();
                var cmd =
                    new SqlCommand(
                        "select  coalesce(sum([Payback Points]),0) from [Payback_Details] where [Mobile No]='" +
                        (string)Session["xuser"] + "'", cn);

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    xavailablepoints.InnerText = "AVAILABLE POINTS TO REDEEM: " + dr[0].ToString();
                    availablePoint = dr[0].ToString();
                }
                cn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
            }
        }
        else
        {
            xavailablepoints.InnerText = "LOGIN TO USE PAYBACK";
        }
    }

    public void LoadcartandDelete(string id)
    {
        string data = "";
        string total = "";
        string count = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();

            var cmddel = new SqlCommand("delete [xsales slave] where [id]='" + id + "'", cn);
            cmddel.ExecuteNonQuery();

            var cmd = new SqlCommand("SELECT [Menu id],[Menu Name],[Quantity],[Sell Price],[total sale amount],[id],[customer flavour],[Weight] FROM [XSales Slave] where [Transaction]='" + (string)Session["xtran"] + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var imagePath = "images/svg-icons/img-placeholder.svg";
                    string[] filelist = Directory.GetFiles(Server.MapPath(Program.ImagePath), dr[1].ToString() + "*.jpg");
                    if (filelist.Length > 0)
                    {
                        imagePath = string.Concat(Program.ImagePath, Path.GetFileName(filelist[0]));
                    }
                    if (dr[6].ToString() == "")
                        data += " <div class=\"row\"> <div class=\"col-md-2\"> <img alt=\"" + dr[1].ToString() + "\" src=\"" + imagePath + "\" class=\"cartItemIcon\" /> </div> <div class=\"col-md-7\"> <div id=\"xmenumane\" class=\"xmenuname\"> " + dr[1].ToString() + " </div> <div id=\"xqty\"> <div class=\"col-md-3 itemDetailCard\">QTY : " + dr[2].ToString() + "</div> <div class=\"col-md-4 itemDetailCard\">PRICE : Rs " + dr[3].ToString() + "</div> <div class=\"col-md-5 itemDetailCard\">AMOUNT : Rs " + dr[4].ToString() + "</div> </div> </div> <a href=\"sparkcart.aspx?del=" + dr[5].ToString() + "\" class=\"removeItemImage\"> <img src=\"images/remove.png\" alt=\"Remove\">  </img></a> </div>";
                    else
                        data += " <div class=\"row\"> <div class=\"col-md-2\"> <img alt=\"" + dr[1].ToString() + "\" src=\"" + imagePath + "\" class=\"cartItemIcon\" /> </div> <div class=\"col-md-7\"> <div id=\"xmenumane\" class=\"xmenuname\"> " + dr[1].ToString() + " ( " + dr[6] + " ) " + " </div> <div id=\"xqty\"> <div class=\"col-md-3 itemDetailCard\">QTY : " + dr[2].ToString() + "</div> <div class=\"col-md-4 itemDetailCard\">PRICE : Rs " + dr[3].ToString() + "</div> <div class=\"col-md-5 itemDetailCard\">AMOUNT : Rs " + dr[4].ToString() + "</div> </div> </div> <a href=\"sparkcart.aspx?del=" + dr[5].ToString() + "\" class=\"removeItemImage\"> <img src=\"images/remove.png\" alt=\"Remove\">  </img></a> </div>";
                }
            }
            else
            {
                data = "";
            }

            var cmdtotal = new SqlCommand("select coalesce (sum([total sale amount]),0),coalesce (count([id]),0) from [Xsales slave] where [Transaction]='" + (string)Session["xtran"] + "'", cn);
            var drtotal = cmdtotal.ExecuteReader();
            if (drtotal.Read())
            {
                total = drtotal[0].ToString();
                count = drtotal[1].ToString();
            }

            cn.Close();


            xcartqty = count;

            if (data != "")
            {
                emptycartcontainer.Visible = false;
                xdata.InnerHtml = data;
                xtotal.InnerText = "TOTAL : Rs " + total;
                xsubtotal.InnerText = "SUB TOTAL : Rs " + total;
                cartcontainer.Visible = true;
            }
            else
            {
                cartcontainer.Visible = false;
                emptycartcontainer.Visible = true;
            }

            Session["xcartqty"] = xcartqty;
            Response.Redirect("sparkcart.aspx");
        }
        catch (Exception ex)
        {
            Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
        }
    }
    public void Loadcart()
    {
        string data = "";
        string total = "";
        string count = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT [Menu id],[Menu Name],[Quantity],[Sell Price],[total sale amount],[id],[customer flavour],[Weight] FROM [XSales Slave] where [Transaction]='" + (string)Session["xtran"] + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var imagePath = "images/svg-icons/img-placeholder.svg";
                    string[] filelist = Directory.GetFiles(Server.MapPath(Program.ImagePath), dr[1].ToString() + "*.jpg");
                    if (filelist.Length > 0)
                    {
                        imagePath = string.Concat(Program.ImagePath, Path.GetFileName(filelist[0]));
                    }
                    string flavourText = string.IsNullOrEmpty(dr[6].ToString()) ? "" : " (" + dr[6].ToString() + ")";
                    if (dr[6].ToString() == "")

                        data += string.Format(@"
                         <div class='cart-card' style='display: flex; border: 1px solid #ccc; padding: 10px; margin-bottom: 10px; border-radius: 8px; align-items: center;'>
                             <div class='cart-card-image' style='flex: 1;'>
                                 <img alt='{0}' src='{1}' class='cart-card-icon' style='width: 100%; height: 100%; object-fit: cover;' />
                             </div>
                             <div class='cart-card-body' style='flex: 4; padding-left: 15px;'>
                                 <h5 class='cart-card-title' style='margin: 0 0 5px 0;'>{0} {2}</h5>
                                 <div class='cart-card-quantity' style='margin-bottom: 5px; display: flex; align-items: center; gap: 10px;'>
                                     <a href='sparkcart.aspx?minus={3}' class='quantity-button' style='padding: 2px 10px;'>-</a>
                                     <span>QTY: {4}</span>
                                     <a href='sparkcart.aspx?plus={3}' class='quantity-button' style='padding: 2px 10px;'>+</a>
                                 </div>
                                 <p class='cart-card-text' style='margin: 2px 0;'>PRICE: Rs {5}</p>
                                 <p class='cart-card-text' style='margin: 2px 0;'>AMOUNT: Rs {6}</p>
                             </div>
                             <div class='cart-card-footer' style='flex: 0; text-align: right;'>
                                 <a href='sparkcart.aspx?del={3}' class='remove-item'>
                                     <img src='images/remove.png' alt='Remove' style='width: 35px; height: 35px;' />
                                 </a>
                             </div>
                         </div>",
                            dr[1].ToString(),     // {0} Menu Name
                            imagePath,            // {1} Image Path
                            flavourText,          // {2} Flavour
                            dr[5].ToString(),     // {3} ID
                            dr[2].ToString(),     // {4} Quantity
                            dr[3].ToString(),     // {5} Price
                            dr[4].ToString()      // {6} Total Amount
                        );



                    else
                        data += string.Format(@"
                         <div class='cart-card' style='display: flex; border: 1px solid #ccc; padding: 10px; margin-bottom: 10px; border-radius: 8px; align-items: center;'>
                             <div class='cart-card-image' style='flex: 1;'>
                                 <img alt='{0}' src='{1}' class='cart-card-icon' style='width: 100%; height: 100%; object-fit: cover;' />
                             </div>
                             <div class='cart-card-body' style='flex: 4; padding-left: 15px;'>
                                 <h5 class='cart-card-title' style='margin: 0 0 5px 0;'>{0} {2}</h5>
                                 <div class='cart-card-quantity' style='margin-bottom: 5px; display: flex; align-items: center; gap: 10px;'>
                                     <a href='sparkcart.aspx?minus={3}' class='quantity-button' style='padding: 2px 10px;'>-</a>
                                     <span>QTY: {4}</span>
                                     <a href='sparkcart.aspx?plus={3}' class='quantity-button' style='padding: 2px 10px;'>+</a>
                                 </div>
                                 <p class='cart-card-text' style='margin: 2px 0;'>PRICE: Rs {5}</p>
                                 <p class='cart-card-text' style='margin: 2px 0;'>AMOUNT: Rs {6}</p>
                             </div>
                             <div class='cart-card-footer' style='flex: 0; text-align: right;'>
                                 <a href='sparkcart.aspx?del={3}' class='remove-item'>
                                     <img src='images/remove.png' alt='Remove' style='width: 35px; height: 35px;' />
                                 </a>
                             </div>
                         </div>",
                            dr[1].ToString(),     // {0} Menu Name
                            imagePath,            // {1} Image Path
                            flavourText,          // {2} Flavour
                            dr[5].ToString(),     // {3} ID
                            dr[2].ToString(),     // {4} Quantity
                            dr[3].ToString(),     // {5} Price
                            dr[4].ToString()      // {6} Total Amount
                        );
                }
            }
            else
            {
                data = "";
            }

            var cmdtotal = new SqlCommand("select coalesce (sum([total sale amount]),0),coalesce (count([id]),0) from [Xsales slave] where [Transaction]='" + (string)Session["xtran"] + "'", cn);
            var drtotal = cmdtotal.ExecuteReader();
            if (drtotal.Read())
            {
                total = drtotal[0].ToString();
                count = drtotal[1].ToString();
            }

            cn.Close();

            xcartqty = count;

            if (data != "")
            {
                emptycartcontainer.Visible = false;
                xdata.InnerHtml = data;
                xtotal.InnerText = "TOTAL : Rs " + total;
                xsubtotal.InnerText = "SUB TOTAL : Rs " + total;
                cartcontainer.Visible = true;
            }
            else
            {
                cartcontainer.Visible = false;
                emptycartcontainer.Visible = true;
            }

            Session["xcartqty"] = xcartqty;
        }
        catch (Exception ex)
        {
            Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void ImageButton1_Click1(object sender, ImageClickEventArgs e)
    {

    }

    private double loyaltyDiscountPercentage;
    public void LoadPaybackDiscount()
    {
        if (Session["xuser"] != null)
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                cn.Open();
                var cmd = new SqlCommand("select [Data] from [Mastercodes] where [Options]='LoyaltyPointDiscount' and [Enabled]='1'", cn);

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    loyaltyDiscountPercentage = Convert.ToDouble(dr[0].ToString());
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
            }
        }
    }

    // Redeem Points Button Click
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        string errorreason = "";

        if (Session["xuser"] != null)
        {
            if (!string.IsNullOrEmpty(txtpayback.Value))
            {
                discountAmount = Convert.ToDouble(Session["DiscountAmount"].ToString());
                if (discountAmount == 0)
                {
                    // Remove gift card if applied
                    Session["xgiftcarddetails"] = null;
                    Session["xgiftcardpoint"] = null;
                    txtGift.Value = "";

                    string fg = xavailablepoints.InnerText.Replace("AVAILABLE POINTS TO REDEEM: ", "");
                    string totalcv = xsubtotal.InnerText.Replace("SUB TOTAL : Rs ", "");
                    double total = double.Parse(totalcv);
                    double customerpoints = double.Parse(txtpayback.Value);
                    double availablepoints = double.Parse(fg);

                    if (availablepoints == 0)
                    {
                        errorreason = "No Points";
                        txtpayback.Value = "";
                    }
                    else
                    {
                        if (availablepoints < 300)
                        {
                            availablepoints = 0;
                            errorreason = "Sorry Available Points must be more than 300 to Redeem";
                            txtpayback.Value = "";
                        }

                        if (availablepoints > 299)
                        {
                            availablepoints = (int)(availablepoints / 2);
                            double b = Math.Round(total, 0);
                            if (availablepoints > (int)b * 2)
                            {
                                availablepoints = (int)b * 2;
                            }
                        }
                    }

                    if (availablepoints == 0)
                    {
                        WebMsgBox.Show(errorreason);
                    }
                    else
                    {
                        if (customerpoints > availablepoints)
                        {
                            WebMsgBox.Show("Sorry You Can Use Maximum " + availablepoints);
                            txtpayback.Value = "";
                        }
                        else
                        {
                            LoadPaybackDiscount();
                            double amount = customerpoints * loyaltyDiscountPercentage / 100;
                            discountAmount = amount;
                            xpaybackpoint = customerpoints.ToString();
                            Session["DiscountAmount"] = discountAmount;
                            Session["DiscountSource"] = "Redeem";
                            Session["RedeemPointsUsed"] = txtpayback.Value;

                            RecalculateTotal();

                            xdiscountBox.Visible = true;
                            lblDiscountReason.Text = "Redeem Discount: Rs " + discountAmount;
                        }
                    }
                }
                else
                {
                    SetToastMessage("Discount Already Applied.", "red");
                }
            }
            else
            {
                txtpayback.Value = "";
                SetToastMessage("Please Enter Some Points", "red");
            }
        }
        else
        {
            WebMsgBox.Show("Please Login To View Available Points");
        }
    }

    protected void RedirectShopping_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    private int GetAllowedCouponCount(double totalAmount)
    {
        int allowedCoupons = 0;
        using (var cn = new SqlConnection(Program.Connection2))
        {
            cn.Open();
            var cmd = new SqlCommand("SELECT [options], [data] FROM MasterCodes WHERE Enabled = 1 AND [options] LIKE 'coupon|%'", cn);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string options = reader["options"].ToString().Trim();
                    string[] parts = options.Split('|');

                    if (parts.Length >= 2 && parts[0].ToLower() == "coupon")
                    {
                        double min = Convert.ToDouble(parts[1]);
                        double max;
                        double maxVal;
                        if (parts.Length > 2 && double.TryParse(parts[2], out maxVal))
                        {
                            max = maxVal;
                        }
                        else
                        {
                            max = double.MaxValue;
                        }

                        if (totalAmount >= min && totalAmount <= max)
                        {
                            allowedCoupons = Convert.ToInt32(reader["data"]);
                            break;
                        }
                    }
                }
            }
        }
        return allowedCoupons;
    }
    protected void CheckGiftCard_Click(object sender, EventArgs e)
    {
        if (Session["xuser"] == null)
        {
            WebMsgBox.Show("Please Login To Use Gift Card");
            return;
        }

        string cardNumber = txtGift.Value.Trim();
        if (string.IsNullOrEmpty(cardNumber))
        {
            SetToastMessage("Please enter a gift card code.", "red");
            return;
        }

        if (Session["DiscountSource"] != null && Session["DiscountSource"].ToString() == "Redeem")
        {
            SetToastMessage("Remove redeem points to apply gift card.", "red");
            return;
        }

        string subtotalText = xsubtotal.InnerText.Replace("SUB TOTAL : Rs ", "").Trim();
        double subtotalAmount = string.IsNullOrEmpty(subtotalText) ? 0 : Convert.ToDouble(subtotalText);

        int maxAllowedCoupons = GetAllowedCouponCount(subtotalAmount);
        var giftCards = Session["RedeemedGiftCards"] as List<GiftCard> ?? new List<GiftCard>();

        if (maxAllowedCoupons == 0)
        {
            SetToastMessage("Total Amount is Not eligible for Coupon.", "orange");
            return;
        }
        if (giftCards.Count >= maxAllowedCoupons)
        {
            SetToastMessage("Only " + maxAllowedCoupons + " gift card(s) allowed for this amount.", "orange");
            return;
        }

        if (giftCards.Any(g => g.CardNumber.Equals(cardNumber, StringComparison.OrdinalIgnoreCase)))
        {
            SetToastMessage("Gift card already applied.", "orange");
            return;
        }

        using (var cn = new SqlConnection(Program.Connection2))
        {
            cn.Open();

            var cmd = new SqlCommand(" SELECT [value] FROM [GiftCardDetail] WHERE [GiftCode]=@code AND " +
                "CONVERT(datetime,[FromDate],105) <= CONVERT(datetime,@now,105) AND CONVERT(datetime,[ToDate],105) >= CONVERT(datetime,@now,105)AND [Status]='ACTIVE'", cn);

            cmd.Parameters.AddWithValue("@code", cardNumber);
            cmd.Parameters.AddWithValue("@now", DateTime.Now.ToString("dd-MM-yyyy"));

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    double value = Convert.ToDouble(dr["value"]);

                    giftCards.Add(new GiftCard { CardNumber = cardNumber, Value = value });
                    Session["RedeemedGiftCards"] = giftCards;

                    Session["DiscountSource"] = "GiftCard";
                    UpdateGiftCardDiscount();

                    SetToastMessage("Gift card applied successfully!", "green");
                }
                else
                {
                    SetToastMessage("Invalid or expired gift card.", "red");
                }
            }
        }

        txtGift.Value = "";
        BindGiftCards();
    }
    private void UpdateGiftCardDiscount()
    {
        var giftCards = Session["RedeemedGiftCards"] as List<GiftCard> ?? new List<GiftCard>();

        if (giftCards.Any())
        {
            double totalGiftCardValue = giftCards.Sum(g => g.Value);
            Session["DiscountAmount"] = totalGiftCardValue;
            Session["DiscountSource"] = "GiftCard";
            Session["xgiftcardpoint"] = totalGiftCardValue;
            RecalculateTotal();

            lblDiscountReason.Text = "Gift Card Discount: Rs " + totalGiftCardValue;
            xdiscountBox.Visible = true;
        }
    }
    // Remove Discount
    protected void RemoveDiscount_Click(object sender, EventArgs e)
    {
        // Clear all discount sessions
        Session["DiscountSource"] = "";
        Session["DiscountAmount"] = "0";
        Session["RedeemPointsUsed"] = "0";
        Session["GiftCardCode"] = null;
        Session["xgiftcarddetails"] = null;
        Session["xgiftcardpoint"] = null;
        Session["xpaybackpoint"] = 0;
        xpaybackpoint = "0";
        List<GiftCard> giftCards = new List<GiftCard>();
        Session["RedeemedGiftCards"] = giftCards;
        BindGiftCards();
        discountAmount = 0;

        txtpayback.Value = "";
        txtGift.Value = "";

        xdiscountBox.Visible = false;

        RecalculateTotal();
    }
    private void RecalculateTotal()
    {
        string totalcv = xsubtotal.InnerText.Replace("SUB TOTAL : Rs ", "");
        double subtotal = double.Parse(totalcv);
        double total = subtotal;

        if (Session["DiscountAmount"] != null)
        {
            double discount = Convert.ToDouble(Session["DiscountAmount"]);
            total -= discount;
        }

        xtotal.InnerText = "TOTAL : Rs " + Math.Round(total, 0);
    }
    private void SetToastMessage(string message, string Color)
    {
        litMessage.ForeColor = System.Drawing.Color.FromName(Color);
        litMessage.Text = message;
    }
    protected void ImageButton1_Click3(object sender, EventArgs e)
    {
        if (Session["xtran"] != null)
        {
            string yyy = xtotal.InnerText.Replace("TOTAL : Rs ", "");
            double tot = double.Parse(yyy);
            Session["xpaybackpoint"] = xpaybackpoint;
            var redeemedGiftCards = Session["RedeemedGiftCards"] as List<GiftCard> ?? new List<GiftCard>();
            string giftCardCodes = string.Join(",", redeemedGiftCards.Select(gc => gc.CardNumber));
            Session["xgiftcarddetails"] = giftCardCodes;

            if (tot >= Program.minimumOrderAmt)
            {
                if (Session["xuser"] != null)
                {
                    Response.Redirect("sparknext.aspx");
                }
                else
                {
                    Response.Redirect("login.aspx?xs=login");
                }
            }
            else
            {

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
    private void BindGiftCards()
    {
        var giftCards = Session["RedeemedGiftCards"] as List<GiftCard> ?? new List<GiftCard>();

        giftCardDisplaySection.Visible = giftCards.Any();
        rptGiftCards.DataSource = giftCards;
        rptGiftCards.DataBind();
    }
    public class GiftCard
    {
        public string CardNumber { get; set; }
        public double Value { get; set; }
    }

    protected void RemoveGiftCard_Click(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        string cardNumber = "";

        if (btn != null)
        {
            cardNumber = btn.CommandArgument;
        }

        if (!string.IsNullOrEmpty(cardNumber))
        {
            var giftCards = Session["RedeemedGiftCards"] as List<GiftCard>;
            if (giftCards != null)
            {
                giftCards.RemoveAll(g => g.CardNumber.Equals(cardNumber, StringComparison.OrdinalIgnoreCase));
                Session["RedeemedGiftCards"] = giftCards;

                if (giftCards.Count > 0)
                {
                    UpdateGiftCardDiscount();
                }
                else
                {
                    Session["DiscountAmount"] = 0.0;
                    Session["DiscountSource"] = null;
                    xdiscountBox.Visible = false;
                }

                RecalculateTotal();
                BindGiftCards();
            }
        }
    }

}