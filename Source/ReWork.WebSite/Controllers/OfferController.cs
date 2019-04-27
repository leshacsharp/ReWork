﻿using Microsoft.AspNet.Identity;
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
        public void AcceptOffer(int jobId, string employeeId)
        {
            _offerService.AcceptOffer(jobId, employeeId);
            _commitProvider.SaveChanges();
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            return PartialView(new CreateOfferViewModel() { JobId = id });
        }


        [HttpPost]
        public ActionResult Create(CreateOfferViewModel createModel)
        {
            if(!ModelState.IsValid)
            {
                return PartialView(createModel);
            }

            string userId = User.Identity.GetUserId();

            CreateOfferParams createOfferParams = new CreateOfferParams()
            { EmployeeId = userId, JobId = createModel.JobId, Text = createModel.Text,  ImplementationDays = createModel.DaysToImplement, OfferPayment = createModel.OfferPayment };

            _offerService.CreateOffer(createOfferParams);
            _commitProvider.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}