﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Account;
using ReWork.Model.ViewModels.FeedBack;
using ReWork.Model.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUserService _userService;
        private IFeedBackService _feedBackService;
        private ICommitProvider _commitProvider;

        public ProfileController(IUserService userService, IFeedBackService feedBackService, ICommitProvider commitProvider)
        {
            _userService = userService;
            _feedBackService = feedBackService;
            _commitProvider = commitProvider;
        }

        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [HttpGet]
        public ActionResult CreateFeedBack(string reciverId, int jobId)
        {
            var createViewModel = new CreateFeedBackViewModel() { ReciverId = reciverId, JobId = jobId };
            return View(createViewModel);
        }


        [HttpPost]
        public ActionResult CreateFeedBack(CreateFeedBackViewModel createModel)
        {
            if(!ModelState.IsValid)
            {
                return View(createModel);
            }

            string senderId = User.Identity.GetUserId();
            var createParams = new CreateFeedBackParams()
            {
                ReciverId = createModel.ReciverId,
                SenderId = senderId,
                JobId = createModel.JobId,
                Text = createModel.Text,
                QualityOfWork = createModel.QualityOfWork
            };

            //TODO: сделать уведомления для отзывов

            _feedBackService.CreateFeedBack(createParams);
            _commitProvider.SaveChanges();

            return RedirectToAction("Jobs", "Job");
        }

        [HttpGet]
        public ActionResult FeedBacks()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RecivedFeedBacks(string userId)
        {
            IEnumerable<FeedBackInfo> feedbacks = _feedBackService.FindRecivedFeedBacks(userId);

            return Json(feedbacks);
        }



        [HttpGet]
        public ActionResult Information()
        {
            string userId = User.Identity.GetUserId();
            User user = _userService.FindUserById(userId);
            return View(user);
        }

        public ActionResult UserInformation()
        {
            string userId = User.Identity.GetUserId();
            User user = _userService.FindUserById(userId);

            ShortUserInfoViewModel userInfo = new ShortUserInfoViewModel()
            {
                Id = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateRegistration = (DateTime)user.RegistrationdDate,
                Image = user.Image
            };

            var feedBacks = user.RecivedFeedBacks?.ToList();
            int countFeedBacks = feedBacks != null ? feedBacks.Count() : 0;


            int countFeedbacksForPer = countFeedBacks == 0 ? 1 : feedBacks.Count();
            double percentPositiveFeedBacks = (double)feedBacks.Count(p => (int)p.QualityOfWork >= 3) * 100 / countFeedbacksForPer;

            userInfo.CountFeedbacks = countFeedBacks;
            userInfo.PercentPositiveFeedbacks = (int)Math.Round(percentPositiveFeedBacks);

            return PartialView(userInfo);
        }



        [HttpPost]
        public void ChangeProfileType(ProfileType profile)
        {
            HttpCookie profileCookie = Request.Cookies["profile"];

            profileCookie.Value = Enum.GetName(typeof(ProfileType), profile);
            profileCookie.Expires = DateTime.UtcNow.AddYears(1);
            Response.Cookies.Add(profileCookie);
        }

        private void AddModeErrors(IdentityResult result)
        {
            foreach (var msg in result.Errors)
            {
                ModelState.AddModelError("", msg);
            }
        }
    }
}