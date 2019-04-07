using Microsoft.AspNet.Identity;
using ReWork.Common;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private ICustomerProfileService _customerService;
        private IEmployeeProfileService _employeeService;
        private ISectionService _sectionService;

        public SettingsController(ICustomerProfileService customerService, IEmployeeProfileService employeeService, ISectionService sectionService)
        {
            _customerService = customerService;
            _employeeService = employeeService;
            _sectionService = sectionService;
        }

        public ActionResult Account()
        {
            return View();
        }

        [HttpPost]
        public void Customer()
        { 
            _customerService.CreateCustomerProfile(User.Identity.Name);
        }


        [HttpGet]
        public ActionResult Employee()
        {
            IEnumerable<Section> sections = _sectionService.GetAll();
            ViewBag.Sections = sections;

            return View();
        }


        [HttpPost]
        public ActionResult Employee(EmployeeProfileViewModel employee)
        {
            EmployeeProfile param = Mapping<EmployeeProfileViewModel, EmployeeProfile>.MapObject(employee);
            param.User = new User() { UserName = User.Identity.Name };

            _employeeService.CreateEmployeeProfile(param);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CustomerProfileExists()
        {
            bool exists = _customerService.CustomerProfileExists(User.Identity.Name);
            return Json(exists);
        }

        [HttpPost]
        public ActionResult EmployeeProfileExists()
        {
            bool exists = _employeeService.EmployeeProfileExists(User.Identity.Name);
            return Json(exists);
        }
    }
}