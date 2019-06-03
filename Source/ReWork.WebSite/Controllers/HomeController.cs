using ReWork.Model.ViewModels;
using ReWork.Model.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Search()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ChangeCulture(Culture lang)
        {
            HttpCookie cultureCookie = Request.Cookies["lang"];

            cultureCookie.Value = Enum.GetName(typeof(Culture), lang);
            cultureCookie.Expires = DateTime.UtcNow.AddYears(1);
            Response.Cookies.Add(cultureCookie);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    } 
}