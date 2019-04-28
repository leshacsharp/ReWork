using ReWork.Model.ViewModels;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeCulture(string lang)
        {
            string cultureName = String.Empty;
            cultureName = lang != null ? lang : "en";

            string[] cultures = new string[] { "en", "ru" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "en";
            }

            HttpCookie cultureCookie = Request.Cookies["lang"];

            if(cultureCookie != null)
            {
                cultureCookie.Value = cultureName;
            }
            else
            {
                cultureCookie = new HttpCookie("lang", cultureName);
                cultureCookie.Expires = DateTime.Now.AddYears(1);
            }

            Response.Cookies.Add(cultureCookie);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    } 
}