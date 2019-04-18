using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
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
        private ICommitProvider _commitProvider;

        public CustomerController(ICustomerProfileService customerService, ICommitProvider commitProvider)
        {
            _customerService = customerService;
            _commitProvider = commitProvider;
        }

        [HttpPost]
        public ActionResult Create()
        {
            _customerService.CreateCustomerProfile(User.Identity.Name);
            _commitProvider.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [HttpPost]
        public void Delete(string id)
        {
            _customerService.DeleteCustomerProfile(id);
            _commitProvider.SaveChanges();
        }

       
        [HttpPost]
        public ActionResult CustomerProfileExists()
        {
            bool exists = _customerService.CustomerProfileExists(User.Identity.Name);
            return Json(exists);
        }
    }
}