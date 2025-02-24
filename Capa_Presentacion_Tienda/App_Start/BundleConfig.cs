using System.Web;
using System.Web.Optimization;

namespace Capa_Presentacion_Tienda
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.7.1.js"   
            ));

            bundles.Add(new Bundle("~/bundles/Complementos").Include(
                "~/Scripts/fontawesome/all.min.js",
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/js/scripts.js"
            ));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.js"
            ));

            bundles.Add(new Bundle("~/Content/css").Include(
                "~/css/site.css",
                "~/Content/DataTable/css/jquery.dataTables.css",
                "~/Content/DataTable/css/responsive.dataTables.css"
            ));




        }


    }
}
