using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Context;
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
        private ICommitProvider _commitProvider;

        public OfferController(IOfferService offerService, ICommitProvider commitProvider)
        {
            _offerService = offerService;
            _commitProvider = commitProvider;
        }

        [HttpPost]
        public void Create(int jobId, string text, int daysToImplement, int offerPayement)
        {
            string userId = User.Identity.GetUserId();

            CreateOfferParams createOfferParams = new CreateOfferParams()
            { EmployeeId = userId, JobId = jobId, Text = text,  ImplementationDays = daysToImplement, OfferPayment = offerPayement };

            _offerService.CreateOffer(createOfferParams);
            _commitProvider.SaveChanges();
        }
    }
}