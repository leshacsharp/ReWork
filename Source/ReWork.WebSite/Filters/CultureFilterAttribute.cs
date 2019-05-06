using ReWork.Model.ViewModels.Common;
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
            Culture currentCulture = Culture.en;
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];

            if(cultureCookie != null)
            {
                currentCulture = (Culture)Enum.Parse(typeof(Culture), cultureCookie.Value); 
            }
            else
            {
                string defaultCulture = Enum.GetName(typeof(Culture), currentCulture);
                cultureCookie = new HttpCookie("lang", defaultCulture);
                cultureCookie.Expires = DateTime.UtcNow.AddYears(1);

                filterContext.HttpContext.Response.Cookies.Add(cultureCookie);
            }

            string cultureName = Enum.GetName(typeof(Culture), currentCulture);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}