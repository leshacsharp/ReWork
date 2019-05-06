using ReWork.Model.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ReWork.WebSite.Filters
{
    public class ProfileFilterAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie profileCookie = filterContext.HttpContext.Request.Cookies["profile"];
            bool isAuthenticated = filterContext.HttpContext.User.Identity.IsAuthenticated;

            if (profileCookie == null && isAuthenticated)
            {
                string defaultProfileName = Enum.GetName(typeof(ProfileType), ProfileType.Customer);

                profileCookie = new HttpCookie("profile", defaultProfileName);
                profileCookie.Expires = DateTime.Now.AddYears(1);

                filterContext.HttpContext.Response.Cookies.Add(profileCookie);
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
          
        } 
    }
}