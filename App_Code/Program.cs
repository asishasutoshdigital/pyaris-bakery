using System.Configuration;
/// <summary>
/// Summary description for Program
/// </summary>
public class Program
{
    public Program()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string Connection = ConfigurationManager.AppSettings["Connection"];
    public static string Connection2 = ConfigurationManager.AppSettings["Connection2"];
    public static string PaymentGatewayURL = ConfigurationManager.AppSettings["PaymentGatewayURL"];
    public static string PaymentGateway = ConfigurationManager.AppSettings["PaymentGateway"];
    public static int minimumOrderAmt = int.Parse(ConfigurationManager.AppSettings["minimumOrderAmt"]);
    public static bool isPaybackAvailable = bool.Parse(ConfigurationManager.AppSettings["isPaybackAvailable"]);
    public static string unavailableOutlets = ConfigurationManager.AppSettings["unavailableOutlets"];
    public static string unavailablePincodes = ConfigurationManager.AppSettings["unavailablePincodes"];
    public static string SMS_Url = ConfigurationManager.AppSettings["SMS_Url"];
    public static string SMS_UserName = ConfigurationManager.AppSettings["SMS_UserName"];
    public static string SMS_Password = ConfigurationManager.AppSettings["SMS_Password"];
    public static string SMS_Senderid = ConfigurationManager.AppSettings["SMS_Senderid"];
    public static string SMS_Channel = ConfigurationManager.AppSettings["SMS_Channel"];
    public static string SMS_DSC = ConfigurationManager.AppSettings["SMS_DSC"];
    public static string flashsms = ConfigurationManager.AppSettings["flashsms"];
    public static string SMS_route = ConfigurationManager.AppSettings["SMS_route"];
    public static string SMS_PEID = ConfigurationManager.AppSettings["SMS_PEID"];
    public static string SMS_Template_NewOrder = ConfigurationManager.AppSettings["SMS_Template_NewOrder"];
    public static string SMS_Template_Order_Placing = ConfigurationManager.AppSettings["SMS_Template_Order_Placing"];
    public static string SMS_Template_Web_Order_Update = ConfigurationManager.AppSettings["SMS_Template_Web_Order_Update"];
    public static string SMS_Template_Order_Update = ConfigurationManager.AppSettings["SMS_Template_Order_Update"];

    public static string PhonepeCallbackUrl = ConfigurationManager.AppSettings["PhonepeCallbackUrl"];
    public static string SelectedGroup = ConfigurationManager.AppSettings["SelectedGroup"];
    public static string SelectedSubGroup = ConfigurationManager.AppSettings["SelectedSubGroup"];
    public static string CCAvenue_redirecturl = ConfigurationManager.AppSettings["CCAvenue_redirecturl"];
    public static string CCAvenue_cancelurl = ConfigurationManager.AppSettings["CCAvenue_cancelurl"];
    public static string CCAvenue_merchantid = ConfigurationManager.AppSettings["CCAvenue_merchantid"];
    public static string CCAvenue_currency = ConfigurationManager.AppSettings["CCAvenue_currency"];
    public static string CCAvenue_language = ConfigurationManager.AppSettings["CCAvenue_language"];
    public static string CCAvenue_billingcountry = ConfigurationManager.AppSettings["CCAvenue_billingcountry"];
    public static string CCAvenue_workingKey = ConfigurationManager.AppSettings["CCAvenue_workingKey"];
    public static string CCAvenue_access = ConfigurationManager.AppSettings["CCAvenue_access"];
    public static string Admin_PhoneNumber = ConfigurationManager.AppSettings["Admin_PhoneNumber"];
    public static string Customercare_Emailid = ConfigurationManager.AppSettings["Customercare_Emailid"];
    public static string Admin_SecondaryPhoneNumber = ConfigurationManager.AppSettings["Admin_SecondaryPhoneNumber"];
    public static string Email_Environment = ConfigurationManager.AppSettings["Email_Environment"];
    public static string EmailServer_Password = ConfigurationManager.AppSettings["EmailServer_Password"];
    public static string EmailServer_UserName = ConfigurationManager.AppSettings["EmailServer_UserName"];
    public static string Display_Image_Max = ConfigurationManager.AppSettings["Display_Image_Max"];
    public static string ImagePath = ConfigurationManager.AppSettings["ImagePath"];

    public static string ParisApiUrl = ConfigurationManager.AppSettings["ParisApiUrl"];
    public static string ParisApiKey = ConfigurationManager.AppSettings["ParisApiKey"];
    public static string ParisApiDistanceKey = ConfigurationManager.AppSettings["ParisDistanceApiUrl"];
}