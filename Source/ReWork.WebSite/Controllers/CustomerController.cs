using ReWork.Logic.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private ICustomerProfileService _customerService;

        public CustomerController(ICustomerProfileService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public void Customer()
        {
            _customerService.CreateCustomerProfile(User.Identity.Name);
        }


        [HttpPost]
        public ActionResult Delete(string userId)
        {
            _customerService.DeleteCustomerProfile(userId);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [HttpPost]
        public ActionResult CustomerProfileExists()
        {
            bool exists = _customerService.CustomerProfileExists(User.Identity.Name);
            return Json(exists);
        }
    }
}