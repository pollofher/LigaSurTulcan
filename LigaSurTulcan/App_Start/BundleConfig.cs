using System.Web;
using System.Web.Optimization;

namespace LigaSurTulcan
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
           
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                     "~/Content/js/jquery-3.3.1.min.js",
                     "~/Content/js/jquery-migrate-3.0.1.min.js",
                     "~/Content/js/jquery-ui.js",
                     "~/Content/js/popper.min.js",
                     "~/Content/js/bootstrap.min.js",
                     "~/Content/js/owl.carousel.min.js",
                     "~/Content/js/jquery.stellar.min.js",
                     "~/Content/js/jquery.countdown.min.js",
                     "~/Content/js/bootstrap-datepicker.min.js",
                     "~/Content/js/jquery.easing.1.3.js",
                     "~/Content/js/aos.js",
                     "~/Content/js/jquery.fancybox.min.js",
                     "~/Content/js/jquery.sticky.js",
                     "~/Content/js/jquery.mb.YTPlayer.min.js",
                     "~/Content/js/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/owl.carousel.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                      "~/Content/css/owl.theme.default.min.css",
                      "~/Content/css/jquery.fancybox.min.css",
                      "~/Content/css/bootstrap-datepicker.css",
                      "~/Content/fonts/flaticon/font/flaticon.css",
                      "~/Content/css/aos.css",
                      "~/Content/css/jquery.mb.YTPlayer.min.css",
                      "~/Content/css/style.css",
                       "~/Content/all.css"));
        }
    }
}
