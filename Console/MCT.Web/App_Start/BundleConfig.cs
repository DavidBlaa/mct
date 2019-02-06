using System.Web.Optimization;

namespace MCT.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/gantt").Include(
                      "~/Scripts/Gantt/jquery.fn.gantt.min.js",
                      "~/Scripts/Gantt/dataHours.js",
                      "~/Scripts/Gantt/dataDaysEnh.js",
                      "~/Scripts/Gantt/dataDays.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/roadmap").Include(
                     "~/Scripts/roadmap/jquery.roadmap.min.js",
                     "~/Scripts/raodmap/timeline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Scripts/bootstrap.js",
                     "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Gantt/style.css",
                      "~/Content/MCT/mct-patch-planer.css",
                      "~/Content/roadmap/jquery.roadmap.min.css",
                      "~/Content/roadmap/timeline.min.css",
                      "~/Content/Site.css"));
        }
    }
}