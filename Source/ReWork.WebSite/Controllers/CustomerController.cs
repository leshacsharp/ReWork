using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.ViewModels.Customer;
using ReWork.Model.ViewModels.Offer;
using System;
using System.Linq;
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


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var customer = _customerService.FindCustomerProfile(id);
            if (customer == null)
                return View("Error");

            var customerModel = new CustomerDetailsViewModel()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                ImagePath = Convert.ToBase64String(customer.Image),
                UserName = customer.UserName,
                RegistrationdDate = customer.RegistrationdDate,
                CountPublishJobs = customer.CountPublishJobs,
                CountReviews = customer.QualityOfWorks.Count(),
            };

            if (customer.QualityOfWorks.Count() > 0)
                customerModel.AvarageReviewMark = (int)customer.QualityOfWorks.Select(p => (int)p).Average();

            int countFeedbacksForPer = customerModel.CountReviews == 0 ? 1 : customerModel.CountReviews;
            double percentPositiveFeedBacks = (double)customer.QualityOfWorks.Count(p => (int)p >= 3) * 100 / countFeedbacksForPer;
            customerModel.PercentPositiveReviews = (int)Math.Round(percentPositiveFeedBacks);

            return View(customerModel);
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

        [HttpGet]
        public ActionResult MyJobDetails(int id)
        {
            var job = _jobService.FindCustomerJob(id);
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

            var customerOfferModels = from c in customerOffers
                                      select new CustomerOfferViewModel()
                                      {
                                          Id = c.Id,
                                          UserName = c.UserName,
                                          Text = c.Text,
                                          UserDateRegistration = c.UserDateRegistration,
                                          AddedDate = c.AddedDate,
                                          ImplementationDays = c.ImplementationDays,
                                          OfferPayment = c.OfferPayment,
                                          JobId = c.JobId,
                                          JobTitle = c.JobTitle,
                                          EmployeeId = c.EmployeeId,
                                          EmployeeImagePath = Convert.ToBase64String(c.EmployeeImage)
                                      };

            return Json(customerOfferModels);
        }


        [HttpPost]
        public ActionResult ProfileExists(string userId)
        {
            bool exists = _customerService.CustomerProfileExists(userId);
            return Json(exists);
        }
    }
}