using System.Web;
using System.Web.Mvc;

namespace NaumanWebApi {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
