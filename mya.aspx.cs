using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel.MsmqIntegration;
using System.Web.UI.WebControls;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xuser"] != null)
        {

            if (Request.QueryString["logout"] == "yes")
            {
                Session.Remove("xuser");
                Session.Remove("xusername");
                Response.Redirect("Default.aspx", true);
            }


            if (Request.QueryString["cancel"] != "")
            {
                CancelLoad(Request.QueryString["cancel"]);
            }

            //cartqty.InnerText = Request.Cookies["xcartqty"].Value;
            LoadOrders();
        }
        else
        {
            Response.Redirect("login.aspx?mya=a");
        }
    }


    public void CancelLoad(string id)
    {
        var cn = new SqlConnection(Program.Connection);
        string CustomerUser = string.Empty, smsMessage = string.Empty, emailMesssage =string.Empty ,CustomerName =string.Empty;
        try
        {
            if(id!=null)
            { 
                cn.Open();
                var cmd = new SqlCommand("update [Xsales MAster] set [Status]='CANCELLED' where [User]='" + (string)Session["xuser"] + "' and [ID]='" + id + "'", cn);
                cmd.ExecuteNonQuery();

                var cmdDPayBack = new SqlCommand("delete from [Payback_Details]  where [Mobile No]='" + (string)Session["xuser"] + "' and [Order No]='" + id + "' and [Location]='ONLINE'", cn);
                cmdDPayBack.ExecuteNonQuery();

                cn.Close();
                CustomerUser = Util.GetCustomeruser(id);
                CustomerName = Util.Getname(CustomerUser);
                smsMessage = "Hi " + CustomerName + ", Order Update. Your Order " + id + " is Cancelled Successfully, please contact branch for refund if applicable, Paris Bakery(Jam Foods)";
                emailMesssage = "Hi " + CustomerName + ",<br> Sorry your order is Cancelled. please contact branch for refund if applicable,<br>Paris Bakery(Jam Foods)";
                sendSMS.Send(CustomerUser, smsMessage,Program.SMS_Template_Order_Update);
                sendemail.Gox(sendemail.Fetchemail(CustomerUser), "Order is Cancelled", emailMesssage);
                //Inform Pyaris
                string pyarisMessage = "Web Order Update: " + id + ", HAS BEEN CANCELLED. User: " + CustomerName + " - " + CustomerUser + " | Contact - " + CustomerUser;
                sendSMS.Send(Program.Admin_SecondaryPhoneNumber, pyarisMessage,Program.SMS_Template_Web_Order_Update);
                sendSMS.Send(Program.Admin_PhoneNumber, pyarisMessage, Program.SMS_Template_Web_Order_Update);
                sendemail.Gox(Program.Customercare_Emailid, "WEB ORDER CANCELLED", pyarisMessage);
            }
        }
        catch (Exception)
        {

        }
    }

    private double loyaltyDiscountPercentage;

    public void LoadPaybackUsed()
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
    public void LoadOrders()
    {
        string data = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            LoadPaybackUsed();
            cn.Open();
            var cmd = new SqlCommand("select * from [Xsales MAster] where [User]='" + (string)Session["xuser"] + "' order by [id] desc", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    string x = dr["Status"].ToString();

                    string image = "";
                    string cdf = "";

                    if (x == "PLACED")
                    {
                        image = "statuspicX1.png";  
                    }

                    if (x == "ORDER PROCESSING")
                    {
                        image = "statuspicX2.png";
                        cdf = "";
                    }

                    if (x == "ORDER READY / OUT FOR DELIVERY")
                    {
                        image = "statuspicX3.png";
                        cdf = "";
                    }

                    if (x == "ORDER COMPLETED")
                    {
                        image = "statuspicX4.png";
                        cdf = "";
                    }

                    if (x == "CANCELLED")
                    {
                        image = "statuspiccancel.png";
                        cdf = "";
                    }
                    if (x == "REJECTED")
                    {
                        image = "statuspicreject.png";
                        cdf = "";
                    }
                    if(x == "ORDER_RECEIVED")
                    {
                        image = "statuspicX1.png";
                        cdf = "<div class=\"col-md-4\"><a href=\"mya.aspx?cancel=" + dr[1].ToString() + "\"><img src=\"images/cancel.png\" alt=\"Cancel\" /></a></div>";
                    }
                    data += "<div> <div class=\"orderContainer\"> <div class=\"row orderDetailsBox\"> <div class=\"col-md-8\"> <div class=\"OrderNoText\"> Order No: " + dr[1].ToString() + "</div> <div class=\"OrderDetailsText\"> Order Date : " + dr["Date"] + " , Delivery/Pickup Date : " + dr["Order Date"] + " , Delivery/Pickup Time : " + dr["delivery time"] + "</div> </div> " + cdf + " </div> <img src=\"images/" + image + "\" alt=\"Order Status\" /> <div> <table class=\"table table-condensed table-responsive tableContainer\"><thead><tr><th class=\"col-md-4\">Description</th><th class=\"col-md-2\">Quantity</th><th class=\"col-md-2\">Rate</th><th class=\"col-md-2\">Amount</th></tr></thead><tbody>";
                    var cmdx = new SqlCommand("select [Menu Name],[Quantity],[Sell Price],[Total Sale Amount] from [xsales slave] where [Order No]='" + dr[1].ToString() + "'", cn);
                    var drx = cmdx.ExecuteReader();
                    while (drx.Read())
                    {
                        data +=
                            "<tr><td class=\"col-md-4\"> " + drx[0].ToString() + " </td>  <td class=\"col-md-2\"> " + drx[1].ToString() + " </td> <td class=\"col-md-2\"> " + drx[2].ToString() + " </td> <td class=\"col-md-3\"> " + drx[3].ToString() + " </td> </tr>";
                    }

                    string fh = "";
                    string delivery = dr["Delivery"].ToString();
                    if (delivery == "PICKUP FROM OUTLET")
                    {
                        fh = dr["Outlet"].ToString();
                    }
                    else
                    {
                        fh = dr["Address"].ToString();
                    }

                    string paymode = dr["Paymode"].ToString();

                    double paybackamt = double.Parse(dr["Payback Amount"].ToString());
                    double paybackpoint = paybackamt * 100/ loyaltyDiscountPercentage;
                    double giftCardValue = double.Parse(dr["Discount Amount"].ToString());

                    delivery = delivery + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;||&nbsp;&nbsp;&nbsp;&nbsp;(PAYMODE : " + paymode+")";


                        data +=
                            "</tbody></table></div><div class =\"row orderFooterBox\"> <div class=\"col-md-8\"><div class=\"OrderNoText\"> " + delivery + " </div> <div class=\"OrderDetailsText\"> " + fh + " </div></div>  <div class=\"col-md-4\"> <div class=\"smallText\"> SUBTOTAL : Rs " + dr["Total Sale Amount"].ToString() + " </div><div class=\"smallText\">   PAYBACK REDEEM : Rs " + paybackamt + " (" + paybackpoint + " POINTS) </div><div class=\"smallText\">   GIFT CARD : Rs " + giftCardValue + " </div> <div class=\"totalText\">TOTAL: Rs " + dr["Net Total"].ToString() + " (Delivery: Rs " + dr[23].ToString() + ") </div> </div> </div></div></div> ";
                }
            }
            else
            {
                data = "<div class=\"col-md-12 no-orders-container\">No Orders :-(<div>";
            }


            cn.Close();

            xfdata.InnerHtml = data;
        }
        catch (Exception)
        {

        }
    }
}