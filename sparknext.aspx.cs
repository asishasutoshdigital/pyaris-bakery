using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Net;
using RestSharp;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Web.UI;

public partial class spark : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["xtran"] == null || Session["xtran"] == "")
        {

            Session["xtran"] = Randompin.Generate(20);

            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
            Response.Redirect("Default.aspx", true);
        }
        if (!IsPostBack)
        {
            //Session["xcartqty"] = "0";
            RadioButton1.ForeColor = Color.Crimson;
            txtdate.Value = DateTime.Now.ToString("dd-MMM-yyyy");

            ExistingContactRadioBtn.ForeColor = Color.Crimson;
            txtmob.Value = (string)Session["xuser"];
            txtmob.Disabled = true;
            CheckAddess();
            loadcart();
        }
    }
    string giftCardProvider= "";

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
    }
    public void loadcart()
    {
        string total = "";
        string count = "";
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            string paybackpoint = Session["xpaybackpoint"].ToString();
            giftCardProvider = Session["xgiftcarddetails"].ToString();
            LoadPaybackDiscount();
            double paybackamount = Math.Round(double.Parse(paybackpoint) * loyaltyDiscountPercentage/100, 0);

            if (paybackpoint == "0" && giftCardProvider != "")
            {
                paybackamount = Convert.ToDouble(Session["xgiftcardpoint"].ToString());
            }
            var cmdtotal = new SqlCommand("select coalesce (sum([total sale amount]),0),coalesce (count([id]),0) from [Xsales slave] where [Transaction]='" + (string)Session["xtran"] + "'", cn);
            var drtotal = cmdtotal.ExecuteReader();
            if (drtotal.Read())
            {
                total = drtotal[0].ToString();
                count = drtotal[1].ToString();
            }
            cn.Close();
            xtotal.InnerText = "TOTAL : Rs " + (Convert.ToDouble(total)- paybackamount);
            xsubtotal.InnerText = "SUB TOTAL : Rs " + total;
            xcount.InnerHtml = "TOTAL CART ITEMS : " + count;
        }
        catch (Exception ex)
        {
            Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
        }

    }
    public void CheckAddess()
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd = new SqlCommand("select * from [xUser Details] where [Mobile No]='" + (string)Session["xuser"] + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    txtmob.Value = dr["Mobile No"].ToString();
                    txtname.Value = dr["Name"].ToString();
                    txtaddress1.Text = dr["Address1"].ToString();
                    txtaddess2.Text = dr["Address2"].ToString();
                    txtcity.Text = dr["City"].ToString();
                    txtpincode.Value = dr["Pincode"].ToString();
                }
            }

            cn.Close();
        }
        catch (Exception)
        {
        }
    }

    public void Requester(String url)
    {
        try
        {
            var myRequest = (HttpWebRequest)WebRequest.Create(url);
            //set up web request...
            ThreadPool.QueueUserWorkItem(o => { myRequest.GetResponse(); });

            //Go2(url);
        }
        catch (Exception)
        {
        }
    }

    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
    }

    protected void ImageButton1_Click1(object sender, EventArgs e)
    {

        if (Session["xtran"] == null || Session["xtran"] == "")
        {
            Session["xtran"] = Randompin.Generate(20);

            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
            Response.Redirect("Default.aspx", true);
        }
        int timehour = DateTime.UtcNow.AddHours(5.5).Hour;
        DateTime datexx = DateTime.ParseExact(txtdate.Value, "dd-MMM-yyyy", CultureInfo.CurrentCulture);

        int valid = 0;

        if (datexx.Date != DateTime.Today.Date)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                valid = 1;
            }
            else
            {
                valid = 2;

            }
        }
        else
        {
            if (timehour < 18)
            {
                if (DropDownList1.SelectedIndex == 1)
                {
                    if (timehour < 9)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }
                if (DropDownList1.SelectedIndex == 2)
                {
                    if (timehour < 12)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }
                if (DropDownList1.SelectedIndex == 3)
                {
                    if (timehour < 15)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }

                if (DropDownList1.SelectedIndex == 4)
                {
                    if (timehour < 18)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }
            }
        }

        if (NewContactRadioBtn.Checked == true && txtmob.Value == "")
        {
            valid = 3;
        }

        if (valid == 1)
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                int flag = 1;

                if (RadioButton2.Checked == true)
                {
                    if (txtname.Value != "" && txtaddress1.Text != "" && txtcity.Text != "" && txtpincode.Value != "")
                    {
                        //const string deliverable =
                        //    "751001, 751022, 751009, 751010, 751005, 751007, 751020, 751006, 751002, 751011, 760001, 761005, 761001, 760003, 760002, 760004, 760005, 760008, 760010, 758002, 758001, 758014, 769001, 769004, 769015, 769007, 769012, 751018, 751014, 751025, 751017, 751015, 751004, 751003, 751030, 751012, 751008, 756001, 756002, 756003, 755019, 755020, 752002,756001,756002,756003, 752001, 752003, 751024, 754132, 754005, 751021, 753001, 751013, 751023, 753006, 753003, 753008, 753004, 753015, 753009, 754004, 753002, 753010, 753014, 754006, 753013, 753012, 753007, 756100, 756101, 711106, 711105, 711104, 711107, 711108, 711109";

                        const string deliverable = "751001, 751002, 751003, 751004, 751005, 751006, 751007, 751008, 751009, 751010, 751011, 751012, 751013, 751014, 751015, 751016, " +
                            "751017, 751018,751019, 751020, 751021, 751022, 751023, 751024, 751025, 751029, 751030, 751031, 751033, 751034, 751038, 751051";

                        string pin = txtpincode.Value;

                        if (deliverable.Contains(pin))
                        {
                            flag = 1;
                        }
                        else
                        {
                            flag = 0;
                            WebMsgBox.Show("Sorry Cannot Deliver to This Pincode");
                        }
                    }
                    else
                    {
                        flag = 0;
                        WebMsgBox.Show("Name, Address, City and Pincode are required for home delivery");
                    }
                }

                if (RadioButton1.Checked == true)
                {
                    string u = ddloutlet.Value;
                    if (u == "SELECT OUTLET")
                    {
                        flag = 0;
                        WebMsgBox.Show("PLEASE SELECT YOUR PICKUP OUTLET");
                    }
                }



                if (flag == 1)
                {
                    string id = Request.QueryString["id"];

                    string transaction = (string)Session["xtran"];
                    string user = (string)Session["xuser"];

                    string delivery = "";
                    string location = "";
                    string address = "";

                    string date = DateTime.ParseExact(txtdate.Value, "dd-MMM-yyyy", CultureInfo.CurrentCulture).Date.ToString();

                    if (RadioButton1.Checked == true)
                    {
                        delivery = "PICKUP FROM OUTLET";
                        location = ddloutlet.Value;
                        address = "DELIVERYCONTACT : " + txtmob.Value;
                    }

                    double deliverycharge = 0;
                    string timeofdelivery = DropDownList1.SelectedItem.Text;

                    if (RadioButton2.Checked == true)
                    {
                        location = "";
                        delivery = "HOME DELIVERY";
                        address = "NAME : " + txtname.Value + ", ADDRESS : " + txtaddress1.Text + "," +
                                  txtaddess2.Text +
                                  ", CITY : " + txtcity.Text + ", PINCODE : " + txtpincode.Value + " , MOB : " +
                                  user + " , DELIVERYCONTACT : " + txtmob.Value;

                        if (ViewState["finalDeliveryCharges"] != null)
                        {
                            deliverycharge = Convert.ToDouble(ViewState["finalDeliveryCharges"]);
                        }
                    }

                    cn.Open();

                    var cmdtotal =
                        new SqlCommand(
                            "select coalesce (count([id]),0),coalesce (sum([Total Cost Price]),0),coalesce (sum([Total Sale Amount]),0),coalesce (sum([Total Tax Amount]),0) from [Xsales slave] where [Transaction]='" +
                            (string)Session["xtran"] + "'", cn);
                    var drtotal = cmdtotal.ExecuteReader();

                    string paybackpoint = Session["xpaybackpoint"].ToString();
                    LoadPaybackDiscount();


                    double paybackamount = Math.Round(double.Parse(paybackpoint) * loyaltyDiscountPercentage/100, 0);
                    giftCardProvider = Session["xgiftcarddetails"].ToString();

                    if (paybackpoint == "0" && giftCardProvider != "")
                    {
                        paybackamount = Convert.ToDouble(Session["xgiftcardpoint"].ToString());                       
                    }

                    double paybackIncrease = 0;
                    LoadPaybackPoint();
                    if (drtotal.Read())
                    {
                        double tytotal = double.Parse(drtotal[2].ToString());

                        paybackIncrease = Math.Round(tytotal * loyaltyPointAddPercentage / 100, 0);

                        tytotal = tytotal - paybackamount + deliverycharge;

                        if (paybackpoint == "0" && giftCardProvider != "")
                        {
                            var cmd =
                                     new SqlCommand(
                                         "insert into [Xsales master] values ('" + transaction + "','" +
                                         DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH:mm") + "','" +
                                         drtotal[0] + "','" + drtotal[1] + "','" + drtotal[2] + "','" + drtotal[3] + "','GIFT CARD','" + paybackamount + "','" +
                                         user + "','0','" + tytotal + "','CASH-ON-DELIVERY','" + transaction +
                                         "','" + user + "','" + delivery + "','" + date + "','" + location + "','" + address +
                                         "','ORDER_RECEIVED','','" + timeofdelivery + "','" + deliverycharge + "','WEBSITE')", cn);
                            cmd.ExecuteNonQuery();

                            if (!string.IsNullOrEmpty(giftCardProvider))
                            {
                                string[] giftCardCodes = giftCardProvider.Split(',');

                                foreach (string code in giftCardCodes)
                                {
                                    string newCode = code.Trim();
                                    if (!string.IsNullOrEmpty(newCode))
                                    {
                                        var cmd2 = new SqlCommand("UPDATE [GiftCardDetail] SET [Status] = 'EXPIRED' WHERE [GiftCode] = @code", cn);
                                        cmd2.Parameters.AddWithValue("@code", newCode);
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        else
                        {
                            var cmd =
                                     new SqlCommand(
                                         "insert into [Xsales master] values ('" + transaction + "','" +
                                         DateTime.Now.ToString("dd-MM-yyyy") + "','" + DateTime.Now.ToString("HH:mm") + "','" +
                                         drtotal[0] + "','" + drtotal[1] + "','" + drtotal[2] + "','" + drtotal[3] + "','','0','" +
                                         user + "','" + paybackamount + "','" + tytotal + "','CASH-ON-DELIVERY','" + transaction +
                                         "','" + user + "','" + delivery + "','" + date + "','" + location + "','" + address +
                                         "','ORDER_RECEIVED','','" + timeofdelivery + "','" + deliverycharge + "','WEBSITE')", cn);
                            cmd.ExecuteNonQuery();
                        }


                    }

                    string cgid = "1111";

                    var cmd1x = new SqlCommand(
                        "select [ID] from [xSales MAster] where [Transaction]='" + transaction + "'", cn);
                    var dr1x = cmd1x.ExecuteReader();
                    if (dr1x.HasRows)
                    {
                        if (dr1x.Read())
                        {
                            cgid = dr1x[0].ToString();
                        }
                    }

                    var cmdins =
                        new SqlCommand(
                            "update [xSales MAster] set [Order no]='" + cgid + "' where [Transaction]='" + transaction +
                            "'",
                            cn);
                    cmdins.ExecuteNonQuery();

                    var cmdupd =
                        new SqlCommand(
                            "update [xSales slave] set [Order no]='" + cgid + "' where [Transaction]='" + transaction +
                            "'",
                            cn);
                    cmdupd.ExecuteNonQuery();

                    if (paybackpoint != "0")
                    {
                        var cmdpb1 = new SqlCommand("insert into [Payback_Details] values ('" + (string)Session["xuser"] + "','-" + paybackpoint + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "'," +
                            "" + "'" + DateTime.Now.ToString("HH:mm") + "','" + cgid + "','USED','ONLINE')", cn);
                        cmdpb1.ExecuteNonQuery();
                    }

                    var cmdpb2 = new SqlCommand("insert into [Payback_Details] values ('" + (string)Session["xuser"] + "','" + paybackIncrease + "','" + DateTime.Now.ToString("dd-MM-yyyy") + "'," +
                        "" + "'" + DateTime.Now.ToString("HH:mm") + "','" + cgid + "','GAIN','ONLINE')", cn);
                    cmdpb2.ExecuteNonQuery();


                    cn.Close();

                    Session["xcartqty"] = "0";
                    Session["xpaybackpoint"] = "0";

                    Notifications.SaveNotification("A NEW WEB ORDER : " + cgid + ", HAS BEEN RECEIVED.");

                    //string data2 = "HEY!! THANKYOU FOR PLACING AN ORDER.%0A%0AORDER NO : " + cgid +
                    //               ".%0A%0AWE WILL SOON SEND YOU THE ORDER DETAILS.%0A%0AENQUIRY(8AM-9PM) : 00 0000 0000%0A:)";

                    string name = Util.Getname(user);

                    //string data2 = "Hi " + name + ", Thank you for placing your order with us. Your Confirmed Order ID is " + cgid + ".%0A";
                    string data2 = "Hi " + name + ", Thank you for placing your order with us. Your Order ID is " + cgid + ", Paris Bakery (Jam foods)";
                    sendSMS.Send(txtmob.Value, data2,Program.SMS_Template_Order_Placing);

                    //string data23 = "Hi " + name + ", Thank you for placing your order with us. Your Confirmed Order ID is " + cgid + ".\n";
                    string data23 = "Hi " + name + ",<br> Thank you for placing your order with us. Your Order ID is " + cgid + ".<br>Paris Bakery (Jam foods)";

                    sendemail.Gox(sendemail.Fetchemail(user), "ORDER RECEIVED", data23);

                    //Inform Pyaris
                    string pyarisMessage = "A New Web Order :" + cgid + ", Has Been Made. User: " + name + " - " + user;
                    sendSMS.Send(Program.Admin_SecondaryPhoneNumber, pyarisMessage, Program.SMS_Template_NewOrder);
                    sendSMS.Send(Program.Admin_PhoneNumber, pyarisMessage,Program.SMS_Template_NewOrder);
                    sendemail.Gox(Program.Customercare_Emailid, "WEB ORDER RECEIVED", pyarisMessage);

                    Response.Redirect("sparkover.aspx", true);
                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.Message);
            }
        }
        if (valid == 0)
        {
            WebMsgBox.Show("Minimum Order Time : 3 Hours and No Order After 6 PM");
        }
        if (valid == 2)
        {
            WebMsgBox.Show("Please Select Your Time Slot");
        }
        if (valid == 3)
        {
            WebMsgBox.Show("Please fill the delivery contact Number or choose a different option");
        }
    }

    private double loyaltyPointAddPercentage=10;
    public void LoadPaybackPoint()
    {
        if (Session["xuser"] != null)
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                cn.Open();
                var cmd =new SqlCommand("select [Data] from [Mastercodes] where [Options]='LoyaltyPointAdd' and [Enabled]='1'", cn);

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    loyaltyPointAddPercentage = Convert.ToDouble(dr[0].ToString());
                }

                cn.Close();
            }
            catch (Exception ex)
            {
                Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
            }
        }
    }

    private double loyaltyDiscountPercentage = 50;
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

    private double GetDistance(string from, string to)
    {
        double distance;
        try
        {
            RestClient restClient = new RestClient();
            var req = new RestRequest(Program.ParisApiDistanceKey, Method.Get);
            req.AddHeader("Key", Program.ParisApiKey);
            req.AddParameter("fromLocation", from);
            req.AddParameter("toLocation", to);
            var res = restClient.ExecuteAsync(req).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                GetDistance response = JsonConvert.DeserializeObject<GetDistance>(res.Content.ToString());
                if (Convert.ToBoolean(response.Status))
                {
                    string distanceStr = response.Distance;
                    string[] parts = distanceStr.Split(' ');
                    if (parts.Length > 0 && double.TryParse(parts[0].Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out distance))
                    {
                        return distance;
                    }
                }

            }
        }
        catch
        {
            throw (new Exception("Unable to Calculate the distance. Please try again later."));
        }
        return 0;
    }

    private string masterCodeData(string values)
    {
        string data=string.Empty;
        using (var cn = new SqlConnection(Program.Connection))
        {
            try
            {
                cn.Open();
                var cmd = new SqlCommand("select [Data] from [Mastercodes] where [Options]='" + values + "' and [Enabled]='1'", cn);

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data = dr[0].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<input type=\"hidden\" value=\"" + ex.Message + "\">");
            }
        }
        return data;
    }
    private string getAddress()
    {
        string fromLocation = masterCodeData("baseAddress");
        if (string.IsNullOrEmpty(fromLocation))
            fromLocation = "F/3, Chandaka IE, Chandrasekharpur";
        return fromLocation;
    }

    private double getDeliveryCharges()
    {
        string baseDeliveryChargesStr = masterCodeData("baseDeliveryChanges");
        double baseDeliveryCharges;
        if (string.IsNullOrEmpty(baseDeliveryChargesStr) || !double.TryParse(baseDeliveryChargesStr, out baseDeliveryCharges))
        {
            baseDeliveryCharges = 100;
        }
        return baseDeliveryCharges;
    }

    private double getExtraCharge()
    {
        string extraChargePerKMStr = masterCodeData("extraChargePerKM");
        double extraChargePerKM;
        if (string.IsNullOrEmpty(extraChargePerKMStr) || !double.TryParse(extraChargePerKMStr, out extraChargePerKM))
        {
            extraChargePerKM = 15;
        }
        return extraChargePerKM;
    }

    private double baseDeliveryChargeUpToKM()
    {
        string maxDistanceForBaseDeliveryChargeStr = masterCodeData("MaxDistanceForBaseDeliveryCharge");
        double maxDistanceForBaseDeliveryCharge;
        if (string.IsNullOrEmpty(maxDistanceForBaseDeliveryChargeStr) || !double.TryParse(maxDistanceForBaseDeliveryChargeStr, out maxDistanceForBaseDeliveryCharge))
        {
            maxDistanceForBaseDeliveryCharge = 5;
        }
        return maxDistanceForBaseDeliveryCharge;
    }

    //protected void ImageButton2_Click1(object sender, ImageClickEventArgs e)    //{
    //    if (Session["xtran"] == null || Session["xtran"] == "")
    //    {
    //        Session["xtran"] = Randompin.Generate(20);

    //        Session["xcartqty"] = "0";
    //        WebMsgBox.Show("Sorry Your Session Has Been Expired");
    //        Response.Redirect("online.aspx", true);
    //    }
    //    int timehour = DateTime.UtcNow.AddHours(5.5).Hour;
    //    DateTime datexx = DateTime.ParseExact(txtdate.Value, "dd-MM-yyyy", CultureInfo.CurrentCulture);

    //    int valid = 0;

    //    if (datexx.Date != DateTime.Today.Date)
    //    {
    //        if (DropDownList1.SelectedIndex != 0)
    //        {
    //            valid = 1;
    //        }
    //        else
    //        {
    //            valid = 2;
    //        }
    //    }
    //    else
    //    {
    //        if (timehour < 18)
    //        {
    //            if (DropDownList1.SelectedIndex == 1)
    //            {
    //                if (timehour < 9)
    //                {
    //                    valid = 1;
    //                }
    //                else
    //                {
    //                    valid = 0;
    //                }
    //            }
    //            if (DropDownList1.SelectedIndex == 2)
    //            {
    //                if (timehour < 12)
    //                {
    //                    valid = 1;
    //                }
    //                else
    //                {
    //                    valid = 0;
    //                }
    //            }
    //            if (DropDownList1.SelectedIndex == 3)
    //            {
    //                if (timehour < 15)
    //                {
    //                    valid = 1;
    //                }
    //                else
    //                {
    //                    valid = 0;
    //                }
    //            }

    //            if (DropDownList1.SelectedIndex == 4)
    //            {
    //                if (timehour < 18)
    //                {
    //                    valid = 1;
    //                }
    //                else
    //                {
    //                    valid = 0;
    //                }
    //            }
    //        }
    //    }

    //    if (valid == 1)
    //    {
    //        var cn = new SqlConnection(Program.Connection);
    //        try
    //        {
    //            int flag = 1;

    //            if (RadioButton2.Checked == true)
    //            {
    //                const string deliverable =
    //                    "751001, 751022, 751009, 751010, 751005, 751007, 751020, 751006, 751002, 751011, 760001, 761005, 761001, 760003, 760002, 760004, 760005, 760008, 760010, 758002, 758001, 758014, 769001, 769004, 769015, 769007, 769012, 751018, 751014, 751025, 751017, 751015, 751004, 751003, 751030, 751012, 751008, 756001, 756002, 756003, 755019, 755020, 752002,756001,756002,756003, 752001, 752003, 751024, 754132, 754005, 751021, 753001, 751013, 751023, 753006, 753003, 753008, 753004, 753015, 753009, 754004, 753002, 753010, 753014, 754006, 753013, 753012, 753007, 756100, 756101, 700074, 700076, 700073, 711106, 711105, 711104, 711107, 711108, 711109";

    //                string pin = txtpincode.Value;

    //                if (deliverable.Contains(pin))
    //                {

    //                    flag = 1;
    //                }
    //                else
    //                {
    //                    flag = 0;
    //                    WebMsgBox.Show("Sorry Cannot Deliver to This Pincode");
    //                }
    //            }

    //            if (RadioButton1.Checked == true)
    //            {
    //                string u = ddloutlet.Value;
    //                if (u == "SELECT OUTLET")
    //                {
    //                    flag = 0;
    //                    WebMsgBox.Show("PLEASE SELECT YOUR PICKUP OUTLET");
    //                }
    //            }



    //            if (flag == 1)
    //            {
    //                string id = Request.QueryString["id"];

    //                string transaction = (string)Session["xtran"];
    //                string user = (string)Session["xuser"];

    //                string delivery = "";
    //                string location = "";
    //                string address = "";

    //                string date = txtdate.Value;

    //                if (RadioButton1.Checked == true)
    //                {
    //                    delivery = "PICKUP FROM OUTLET";
    //                    location = ddloutlet.Value;
    //                    address = "";
    //                }

    //                double deliverycharge = 0;
    //                string timeofdelivery = DropDownList1.SelectedItem.Text;

    //                if (RadioButton2.Checked == true)
    //                {
    //                    location = "";
    //                    delivery = "HOME DELIVERY";
    //                    address = "NAME : " + txtname.Value + ", ADDRESS : " + txtaddress1.Value + "," +
    //                              txtaddess2.Value +
    //                              ", CITY : " + txtcity.Value + ", PINCODE : " + txtpincode.Value + " , MOB : " +
    //                              txtmob.Value;

    //                    if (DropDownList1.SelectedIndex == 1)
    //                    {
    //                        deliverycharge = 49;
    //                    }
    //                    if (DropDownList1.SelectedIndex == 2)
    //                    {
    //                        deliverycharge = 49;
    //                    }
    //                    if (DropDownList1.SelectedIndex == 3)
    //                    {
    //                        deliverycharge = 49;
    //                    }
    //                    if (DropDownList1.SelectedIndex == 4)
    //                    {
    //                        deliverycharge = 49;
    //                    }
    //                    if (DropDownList1.SelectedIndex == 0)
    //                    {
    //                        timeofdelivery = "12PM-3PM";
    //                        deliverycharge = 49;
    //                    }
    //                }




    //                Response.Cookies["ytransaction"].Value = transaction;
    //                Response.Cookies["ytimeofdelivery"].Value = timeofdelivery;
    //                Response.Cookies["ydeliverycharge"].Value = deliverycharge.ToString();
    //                Response.Cookies["yaddress"].Value = address;
    //                Response.Cookies["ydelivery"].Value = delivery;
    //                Response.Cookies["ylocation"].Value = location;
    //                Response.Cookies["ydate"].Value = date;

    //                cn.Open();
    //                var cmdtotal =
    //                   new SqlCommand(
    //                       "select coalesce (count([id]),0),coalesce (sum([Total Cost Price]),0),coalesce (sum([Total Sale Amount]),0),coalesce (sum([Total Tax Amount]),0) from [Xsales slave] where [Transaction]='" +
    //                       (string)Session["xtran"] + "'", cn);
    //                var drtotal = cmdtotal.ExecuteReader();

    //                string paybackpoint = Session["xpaybackpoint"].ToString();

    //                double paybackamount = Math.Round(double.Parse(paybackpoint) * 0.5, 0);
    //                double tytotal = 0;
    //                if (drtotal.Read())
    //                {
    //                    tytotal = double.Parse(drtotal[2].ToString()); tytotal = tytotal - paybackamount + deliverycharge;
    //                    Response.Cookies["yamount"].Value = tytotal.ToString();
    //                }
    //                var cmdf = new SqlCommand("select * from [XUser Details] where [Mobile No]='" + user + "'", cn);
    //                var df = cmdf.ExecuteReader(); if (df.HasRows)
    //                {
    //                    if (df.Read())
    //                    {
    //                        Response.Cookies["yname"].Value = df[2].ToString();
    //                        Response.Cookies["yphone"].Value = user;
    //                        Response.Cookies["yemail"].Value = df[3].ToString();
    //                    }
    //                }

    //                var cmdxrtdel = new SqlCommand("delete [XSales Pg Mid Process] where [Transaction]='" + transaction + "'", cn);
    //                cmdxrtdel.ExecuteNonQuery();


    //                var cmdxrtins = new SqlCommand("insert into [XSales Pg Mid Process] values ('" + user + "','" + paybackpoint + "','" + deliverycharge + "','" + delivery + "','" + date + "','" + location + "','" + address + "','" + timeofdelivery + "','" + transaction + "')", cn);
    //                cmdxrtins.ExecuteNonQuery();

    //                cn.Close();
    //                Response.Redirect("vpayinit.aspx");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            WebMsgBox.Show(ex.Message);
    //        }
    //    }
    //    if (valid == 0)
    //    {
    //        WebMsgBox.Show("Minimum Order Time : 3 Hours and No Order After 6 PM");
    //    }
    //    if (valid == 2)
    //    {
    //        WebMsgBox.Show("Please Select Your Time Slot");
    //    }
    //}

    protected void ImageButton3_Click1(object sender, EventArgs e)
    {
        //Response.Cookies["Discount_Provider"].Value="";
        //Response.Cookies["Discount"].Value="0";
        if (Session["xtran"] == null || Session["xtran"] == "")
        {
            Session["xtran"] = Randompin.Generate(20);

            Session["xcartqty"] = "0";
            WebMsgBox.Show("Sorry Your Session Has Been Expired");
            Response.Redirect("Default.aspx", true);
        }
        int timehour = DateTime.UtcNow.AddHours(5.5).Hour;
        DateTime datexx = DateTime.ParseExact(txtdate.Value, "dd-MMM-yyyy", CultureInfo.CurrentCulture);

        int valid = 0;

        if (datexx.Date != DateTime.Today.Date)
        {
            if (DropDownList1.SelectedIndex != 0)
            {
                valid = 1;
            }
            else
            {
                valid = 2;
            }
        }
        else
        {
            if (timehour < 18)
            {
                if (DropDownList1.SelectedIndex == 1)
                {
                    if (timehour < 9)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }
                if (DropDownList1.SelectedIndex == 2)
                {
                    if (timehour < 12)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }
                if (DropDownList1.SelectedIndex == 3)
                {
                    if (timehour < 15)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }

                if (DropDownList1.SelectedIndex == 4)
                {
                    if (timehour < 18)
                    {
                        valid = 1;
                    }
                    else
                    {
                        valid = 0;
                    }
                }
            }
        }

        if (NewContactRadioBtn.Checked == true && txtmob.Value == "")
        {
            valid = 3;
        }
        else
        {
            Session["DeliveryContactNumber"] = txtmob.Value;
        }

        if (valid == 1)
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                int flag = 1;
                int promo_code = 0;
                if (RadioButton2.Checked == true)
                {
                    if (txtname.Value != "" && txtaddress1.Text != "" && txtcity.Text != "" && txtpincode.Value != "")
                    {
                        const string deliverable ="751001, 751002, 751003, 751004, 751005, 751006, 751007, 751008, 751009, 751010, 751011, 751012, 751013, 751014, 751015, 751016, 751017,751018, 751019, " +
                            "751020, 751021, 751022, 751023, 751024, 751025, 751026, 751027, 751028, 751030, 753001, 753002, 753003, 753004, 753005, 753007,753008, 753009, 753010, 753012, 753013, 753014,753015";
                        const string devl_pin = "751001, 751022, 751009, 751010, 751005, 751007, 751020, 751006, 751002, 751011, 760001, 761005, 751018, 751014, 751025, 751017, 751015, 751004, 751003, 751030, 751012, 751008";

                        string pin = txtpincode.Value;

                        if (deliverable.Contains(pin))
                        {

                            flag = 1;
                            if (devl_pin.Contains(pin))
                            {
                                promo_code = 1;
                            }
                        }
                        else
                        {
                            flag = 0;
                            WebMsgBox.Show("Sorry Cannot Deliver to This Pincode");
                        }
                    }
                    else
                    {
                        flag = 0;
                        WebMsgBox.Show("Name, Address, City and Pincode are required for home delivery");
                    }
                }

                if (RadioButton1.Checked == true)
                {
                    string u = ddloutlet.Value;
                    if (u.Contains("BBSR"))
                    {
                        promo_code = 1;
                    }
                    if (u == "SELECT OUTLET")
                    {
                        flag = 0;
                        WebMsgBox.Show("PLEASE SELECT YOUR PICKUP OUTLET");
                    }
                }



                if (flag == 1)
                {
                    string id = Request.QueryString["id"];

                    string transaction = (string)Session["xtran"];
                    string user = (string)Session["xuser"];

                    string delivery = "";
                    string location = "";
                    string address = "";

                    string date = DateTime.ParseExact(txtdate.Value, "dd-MMM-yyyy", CultureInfo.CurrentCulture).Date.ToString();

                    if (RadioButton1.Checked == true)
                    {
                        delivery = "PICKUP FROM OUTLET";
                        location = ddloutlet.Value;
                        address = "DELIVERYCONTACT : " + txtmob.Value;
                    }

                    double deliverycharge = 0;
                    string timeofdelivery = DropDownList1.SelectedItem.Text;

                    if (RadioButton2.Checked == true)
                    {
                        location = "";
                        delivery = "HOME DELIVERY";
                        address = "NAME : " + txtname.Value + ", ADDRESS : " + txtaddress1.Text + "," +
                                  txtaddess2.Text +
                                  ", CITY : " + txtcity.Text + ", PINCODE : " + txtpincode.Value + " , MOB : " +
                                  user + " , DELIVERYCONTACT : " + txtmob.Value;

                        if (DropDownList1.SelectedIndex == 1)
                        {
                            deliverycharge = 49;
                        }
                        if (DropDownList1.SelectedIndex == 2)
                        {
                            deliverycharge = 49;
                        }
                        if (DropDownList1.SelectedIndex == 3)
                        {
                            deliverycharge = 49;
                        }
                        if (DropDownList1.SelectedIndex == 4)
                        {
                            deliverycharge = 49;
                        }
                        if (DropDownList1.SelectedIndex == 0)
                        {
                            timeofdelivery = "12PM-3PM";
                            deliverycharge = 49;
                        }
                    }



                    string pay_amount = "0";
                    Response.Cookies["ytransaction"].Value = transaction;
                    Response.Cookies["ytimeofdelivery"].Value = timeofdelivery;
                    Response.Cookies["ydeliverycharge"].Value = deliverycharge.ToString();
                    Response.Cookies["yaddress"].Value = address;
                    Response.Cookies["ydelivery"].Value = delivery;
                    Response.Cookies["ylocation"].Value = location;
                    Response.Cookies["ydate"].Value = date;
                    Response.Cookies["ydeliveryphone"].Value = txtmob.Value;

                    cn.Open();
                    var cmdtotal =
                       new SqlCommand(
                           "select coalesce (count([id]),0),coalesce (sum([Total Cost Price]),0),coalesce (sum([Total Sale Amount]),0),coalesce (sum([Total Tax Amount]),0) from [Xsales slave] where [Transaction]='" +
                           (string)Session["xtran"] + "'", cn);
                    var drtotal = cmdtotal.ExecuteReader();

                    string paybackpoint = Session["xpaybackpoint"].ToString();

                    double paybackamount = Math.Round(double.Parse(paybackpoint) * 0.5, 0);
                    double tytotal = 0;
                    if (drtotal.Read())
                    {
                        tytotal = double.Parse(drtotal[2].ToString()); tytotal = tytotal - paybackamount + deliverycharge;
                        Response.Cookies["yamount"].Value = tytotal.ToString();

                    }
                    var cmdf = new SqlCommand("select * from [XUser Details] where [Mobile No]='" + user + "'", cn);
                    var df = cmdf.ExecuteReader(); if (df.HasRows)
                    {
                        if (df.Read())
                        {
                            Response.Cookies["yname"].Value = df[2].ToString();
                            Response.Cookies["yphone"].Value = user;
                            Response.Cookies["yemail"].Value = df[3].ToString();
                        }
                    }


                    if (promo_code == 1)
                    {
                        Double promo_amount = 0;
                        var original_amount = Response.Cookies["yamount"].Value.ToString();
                        //promo_amount=promoApi.promoAmount(txtPromo.Value.ToString(),Double.Parse( Response.Cookies["yamount"].Value.ToString()),"ONLINE",DateTime.ParseExact(txtdate.Value,"dd-MM-yyyy",CultureInfo.CurrentCulture));
                        if (promo_amount != 0)
                        {
                            Response.Cookies["yamount"].Value = promo_amount.ToString();
                            Response.Cookies["Discount_Provider"].Value = "";// txtPromo.Value.ToString();
                            Response.Cookies["Discount"].Value = (Double.Parse(original_amount.ToString()) - Double.Parse(promo_amount.ToString())).ToString();
                        }
                    }
                    var cmdxrtdel = new SqlCommand("delete [XSales Pg Mid Process] where [Transaction]='" + transaction + "'", cn);
                    cmdxrtdel.ExecuteNonQuery();


                    var cmdxrtins = new SqlCommand("insert into [XSales Pg Mid Process] values ('" + user + "','" + paybackpoint + "','" + deliverycharge + "','" + delivery + "','" + date + "','" + location + "','" + address + "','" + timeofdelivery + "','" + transaction + "')", cn);
                    cmdxrtins.ExecuteNonQuery();

                    cn.Close();

                    Response.Redirect("PaymentGateway.aspx");


                }
            }
            catch (Exception ex)
            {
                WebMsgBox.Show(ex.Message);
            }
        }
        if (valid == 0)
        {
            WebMsgBox.Show("Minimum Order Time : 3 Hours and No Order After 6 PM");
        }
        if (valid == 2)
        {
            WebMsgBox.Show("Please Select Your Time Slot");
        }
        if (valid == 3)
        {
            WebMsgBox.Show("Please fill the delivery contact Number or choose a different option");
        }
    }

    protected void AddressFields_TextChanged(object sender, EventArgs e)
    {
        DropDownList1.SelectedIndex = 0;
        xdlcharge.InnerText = "";
    }
    private double finalDeliveryCharges = 0;
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        const string deliverable = "751001, 751002, 751003, 751004, 751005, 751006, 751007, 751008, 751009, 751010, 751011, 751012, 751013, 751014, 751015, 751016, 751017,751018, 751019, " +
                              "751020, 751021, 751022, 751023, 751024, 751025, 751026, 751027, 751028, 751030, 753001, 753002, 753003, 753004, 753005, 753007,753008, 753009, 753010, 753012, 753013, 753014,753015";

        string pin = txtpincode.Value;

        if (string.IsNullOrEmpty(pin) || !deliverable.Contains(pin))
        {
            xdlcharge.InnerText = "Sorry, cannot deliver to this pincode";
            return;
        }
        if (RadioButton2.Checked == true)
        {
            var toLocation = "ADDRESS : " + txtaddress1.Text + "," +
                  txtaddess2.Text +
                  ", CITY : " + txtcity.Text + ", PINCODE : " + txtpincode.Value;

            string fromLocation = getAddress();
            double baseDeliveryCharges = getDeliveryCharges();
            double extraChargePerKM = getExtraCharge();
            double maxDistanceForBaseDeliveryCharge = baseDeliveryChargeUpToKM();
            double distance = GetDistance(fromLocation, toLocation);

            double deliveryFees = 0;
            if (DropDownList1.SelectedIndex > 0)
            {
                if (distance == 0)
                {
                    xdlcharge.InnerText = "Unable to Calculate the distance. Please try again later.";
                    DropDownList1.SelectedIndex = 0;
                    return;
                }
                else if (distance <= maxDistanceForBaseDeliveryCharge)
                {
                    deliveryFees = baseDeliveryCharges;
                }
                else
                {
                    deliveryFees = baseDeliveryCharges + ((distance - maxDistanceForBaseDeliveryCharge) * extraChargePerKM);
                }
                deliveryFees = Math.Round(deliveryFees);

                ViewState["finalDeliveryCharges"] = deliveryFees;
                xdlcharge.InnerText = "Delivery Charge : Rs " + deliveryFees + "/- (If Home Delivery Chosen)";
            }
            else
            {
                xdlcharge.InnerText = "";
            }
        }
        else
        {
            xdlcharge.InnerText = "";
        }
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton1.Checked)
        {
            RadioButton1.ForeColor = Color.Crimson;
            RadioButton2.ForeColor = Color.Black;
        }
    }

    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
        if (RadioButton2.Checked)
        {
            RadioButton2.ForeColor = Color.Crimson;
            RadioButton1.ForeColor = Color.Black;
        }
    }

    protected void ExistingContact_Clicked(object sender, EventArgs e)
    {
        if (ExistingContactRadioBtn.Checked)
        {
            ExistingContactRadioBtn.ForeColor = Color.Crimson;
            NewContactRadioBtn.ForeColor = Color.Black;
            txtmob.Value = (string)Session["xuser"];
            txtmob.Disabled = true;
        }
    }

    protected void NewContact_Clicked(object sender, EventArgs e)
    {
        if (NewContactRadioBtn.Checked)
        {
            NewContactRadioBtn.ForeColor = Color.Crimson;
            ExistingContactRadioBtn.ForeColor = Color.Black;
            txtmob.Value = "";
            txtmob.Disabled = false;
        }
    }
}