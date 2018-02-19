using System.Web;
using System.Web.Mvc;
using TestWebApp.Filters;

namespace TestWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyAuthAttribute());
        }
    }
}
