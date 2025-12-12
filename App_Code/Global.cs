using System;
using System.Web.Optimization;
using System.Web.Routing;

public class Global : System.Web.HttpApplication
{
    void Application_Start(object sender, EventArgs e)
    {
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        log4net.Config.XmlConfigurator.Configure();
        RouteTable.Routes.MapPageRoute("UrlShortner", "sh/{code}", "~/sh.aspx");
    }
}