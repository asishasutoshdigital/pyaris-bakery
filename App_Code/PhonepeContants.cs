using System.Configuration;
/// <summary>
/// Summary description for PhonepeConstants
/// </summary>
public class PhonepeConstants
{
    public static string PhonepeMerchantId = ConfigurationManager.AppSettings["PhonepeMerchantId"];
    public static string PhonepeSaltKey = ConfigurationManager.AppSettings["PhonepeSaltKey"];
    public static string PhonepeSaltIndex = ConfigurationManager.AppSettings["PhonepeSaltIndex"];
    public static string PhonepeBaseUrl = ConfigurationManager.AppSettings["PhonepeBaseUrl"];
    public static string PhonepePayEndPoint = ConfigurationManager.AppSettings["PhonepePayEndPoint"];
    public static string PhonepeRefundEndPoint = ConfigurationManager.AppSettings["PhonepeRefundEndPoint"];
    public static string PhonepeStatusEndPoint = ConfigurationManager.AppSettings["PhonepeStatusEndPoint"];
}