using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.EntitiesInfo;
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
        private IJobService _jobService;
        private IOfferService _offerService;
        private ICommitProvider _commitProvider;

        public CustomerController(ICustomerProfileService customerService, IJobService jobService, IOfferService offerService, ICommitProvider commitProvider)
        {
            _customerService = customerService;
            _jobService = jobService;
            _offerService = offerService;
            _commitProvider = commitProvider;
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            //Todo: create customer details
            return View();
        }

        [HttpPost]
        public void Delete(string id)
        {
            _customerService.DeleteCustomerProfile(id);
            _commitProvider.SaveChanges();
        }


        [HttpGet]
        public ActionResult MyJobs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MyJobs(DateTime? fromDate)
        {
            string userId = User.Identity.GetUserId();
            var jobs = _jobService.FindCustomerJobs(userId, fromDate);

            return Json(jobs);
        }

        [HttpPost]
        public ActionResult MyJobDetails(int id)
        {
            MyJobInfo job = _jobService.FindCustomerJob(id);
            if (job == null)
                return View("Error");

            return PartialView(job);
        }

        [HttpGet]
        public ActionResult MyOffers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerOffers()
        {
            string userId = User.Identity.GetUserId();
            var customerOffers = _offerService.FindCustomerOffers(userId);

            return Json(customerOffers);
        }



        [HttpPost]
        public ActionResult CustomerProfileExists()
        {
            bool exists = _customerService.CustomerProfileExists(User.Identity.Name);
            return Json(exists);
        }
    }
}