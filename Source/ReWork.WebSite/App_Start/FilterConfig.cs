using ReWork.WebSite.Filters;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggerFilterAttribute());
            filters.Add(new CultureFilterAttribute());
            filters.Add(new ProfileFilterAttribute());
        }
    }
}
