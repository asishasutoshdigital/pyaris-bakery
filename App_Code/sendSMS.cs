using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for sendSMS
/// </summary>
public class sendSMS
{
    public sendSMS()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static void Send(string to, string msg, string templateId = "")
    {
        try
        {
            //string url = Program.SMS_Url+ "?user="+ Program.SMS_UserName + "&password=" +Program.SMS_Password + "&senderid="+Program.SMS_Senderid+
            //   url = url to + "&text=" + msg;

            string url = Program.SMS_Url;

            // Parse the existing query string (if any)
            UriBuilder uriBuilder = new UriBuilder(url);
            NameValueCollection queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);

            // Add new parameters
            queryParams.Add("user", Program.SMS_UserName);
            queryParams.Add("password", Program.SMS_Password);
            queryParams.Add("senderid", Program.SMS_Senderid);
            queryParams.Add("channel", Program.SMS_Channel);
            queryParams.Add("DCS", Program.SMS_DSC);
            queryParams.Add("flashsms", Program.flashsms);
            queryParams.Add("route", Program.SMS_route);
            queryParams.Add("PEID", Program.SMS_PEID);
            queryParams.Add("number", to);
            queryParams.Add("text", msg);
            queryParams.Add("DLTTemplateID", templateId);

            // Update the query string
            uriBuilder.Query = queryParams.ToString();

            string newUrl = uriBuilder.ToString();

            var myRequest = (HttpWebRequest)WebRequest.Create(newUrl);
            //set up web request...
            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    using (var response = (HttpWebResponse)myRequest.GetResponse())
                    {

                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            var objText = reader.ReadToEnd();
                            var resOjb = (SmsResponse)js.Deserialize(objText, typeof(SmsResponse));
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            });
        }
        catch (Exception EX)
        {
            //WebMsgBox.Show(EX.Message);
        }

    }

    public class SmsResponse
    {
        public string id { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string JobId { get; set; }
    }
}