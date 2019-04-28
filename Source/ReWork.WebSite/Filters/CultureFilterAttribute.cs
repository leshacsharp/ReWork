using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Filters
{
    public class CultureFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string cultureName = String.Empty;

            HttpCookie culture = filterContext.HttpContext.Request.Cookies["lang"];
            cultureName = culture != null ? culture.Value : "en";

            string[] cultures = new string[] { "en", "ru" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "en";
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}