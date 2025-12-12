using System.Web.Services;

/// <summary>
/// Summary description for sendf
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class sendf : System.Web.Services.WebService
{

    public sendf()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public void SendMessage(string message)
    {
        sendemail.Gox("customercare@Pyaribakery.com", "INQUIRY", message);
    }


    [WebMethod]
    public void SendLines(string name, string mob, string email, string fathername, string message)
    {
        sendemail.SaveFathersday(name, mob, email, fathername, message);
    }

}
