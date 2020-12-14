// Edrick Tamayo
// Thursday 3:30PM
// 12/18/20
using System.Web;
using System.Web.Mvc;

namespace cis237_assignment6
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
