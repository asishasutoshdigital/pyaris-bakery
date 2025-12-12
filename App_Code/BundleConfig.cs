

using System.Web.Optimization;

public class BundleConfig
{
    public static void RegisterBundles(BundleCollection bundles)
    {
        bundles.Add(new ScriptBundle("~/bundles/GlobalHead").Include(
            "~/js/jquery.min.js",
            "~/js/jquery.lazy.min.js",
            "~/js/jquery.imgzoom.js",
            "~/js/numeric.js",
            "~/js/sweetalert.min.js"));

        bundles.Add(new ScriptBundle("~/bundles/Global").Include(
            "~/js/jquery.min.js",
            "~/libraries/lib.js",
            "~/js/functions.js",
            "~/js/numeric.js",
            "~/js/jquery.lazy.min.js",
            "~/js/jquery.imgzoom.min.js",
            "~/datetimepicker_css.js",
            "~/js/awesomplete.min.js",
            "~/js/jquery.cookie.js",
            "~/js/slick.js"));

        bundles.Add(new StyleBundle("~/Content/GlobalContent").Include(
            "~/sparxdata.css",
            "~/css/sweetalert.css",
            "~/js/awesomplete.css",
            "~/css/extra.css",
            "~/css/slick.css",
            "~/libraries/lib.css",
            "~/css/plugins.css",
            "~/css/navigation-menu.css",
            "~/css/shortcode.css",
            "~/style.css"));

        BundleTable.EnableOptimizations = true;
    }
}
