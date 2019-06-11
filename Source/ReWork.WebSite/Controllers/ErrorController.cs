using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    public class ErrorController : Controller
    {  
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }
    }
}