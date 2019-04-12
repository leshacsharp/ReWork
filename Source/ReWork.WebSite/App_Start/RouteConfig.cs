using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ReWork.WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}",
             defaults: new { controller = "Home", action = "Index"}
          );

            routes.MapRoute(
                name: "Paging",
                url: "{controller}/{action}/{page}",
                defaults: new { controller = "Moderator", action = "Users", page = 1 },
                constraints: new {page = @"\d+" }
             );

            routes.MapRoute(
                name: "Details",
                url: "{controller}/{action}/{userName}",
                defaults: new { controller = "Users", action = "Details", userName = "alex" }
            );
        }
    }
}
