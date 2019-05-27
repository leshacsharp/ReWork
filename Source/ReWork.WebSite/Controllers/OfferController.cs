using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Context;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private IOfferService _offerService;
        private INotificationService _notificationService;
        private ICommitProvider _commitProvider;

        public OfferController(IOfferService offerService, INotificationService notificationService, ICommitProvider commitProvider)
        {
            _offerService = offerService;
            _notificationService = notificationService;
            _commitProvider = commitProvider;
        }


        [HttpGet]
        public ActionResult Create(int id)
        {
            return PartialView(new CreateOfferViewModel() { JobId = id });
        }

        [HttpPost]
        public ActionResult Create(CreateOfferViewModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(createModel);
            }

            string userId = User.Identity.GetUserId();

            CreateOfferParams createOfferParams = new CreateOfferParams()
            { EmployeeId = userId, JobId = createModel.JobId, Text = createModel.Text, ImplementationDays = createModel.DaysToImplement, OfferPayment = createModel.OfferPayment };

            _offerService.CreateOffer(createOfferParams);

            _commitProvider.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        [HttpPost]
        public void AcceptOffer(int offerId, string employeeId)
        {
            _offerService.AcceptOffer(offerId, employeeId);

            string senderId = User.Identity.GetUserId();
            string notifyText = "You have been chosen to work in a project <a href='/employee/myjobs'>you projects</a>";

            _notificationService.CreateNotification(senderId, employeeId, notifyText);
            _commitProvider.SaveChanges();

            _notificationService.RefreshNotifications(employeeId);
        }


        [HttpPost]
        public void RejectOffer(int offerId)
        {
            _offerService.RejectOffer(offerId);
            _commitProvider.SaveChanges();
        }

        [HttpPost]
        public ActionResult OfferExists(int jobId)
        {
            string userId = User.Identity.GetUserId();
            bool offerExists = _offerService.OfferExists(jobId, userId);
            return Json(offerExists);
        }

    }
}