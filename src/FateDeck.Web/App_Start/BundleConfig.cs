using System.Web.Optimization;

namespace FateDeck.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        //"~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/site.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"
            ));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                       // "~/Content/themes/base/all.css",
                        "~/Content/site.css",
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css"
            ));

        }
    }
}