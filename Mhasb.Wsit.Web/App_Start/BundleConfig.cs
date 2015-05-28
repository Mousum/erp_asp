using System.Web;
using System.Web.Optimization;

namespace Mhasb.Wsit.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery-ui.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-toggle.min.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/custome").Include(
                      "~/Scripts/cbpAnimatedHeader.js",
                      "~/Scripts/cbpAnimatedHeader.min.js",
                      "~/Scripts/classie.js",
                      "~/Scripts/jquery.easing.min.js",
                      "~/Scripts/master.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/customeJs.js",
                      "~/Scripts/treeview/jquery-treeview-1.4.0.min.js",
                       "~/Scripts/treeview/jquery-treeview-async-0.1.0.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/master.css",
                       "~/Content/loader.css",
                      "~/Content/font-awesome/css/font-awesome.min.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/jquery-ui.css",
                       "~/Scripts/treeview/jquery-treeview.css"
                      ));
        }
    }
}
