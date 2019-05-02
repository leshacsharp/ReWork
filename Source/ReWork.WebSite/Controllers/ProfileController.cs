using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Account;
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

        public ProfileController(IUserService userService, IFeedBackService feedBackService)
        {
            _userService = userService;
            _feedBackService = feedBackService;
        }

        public IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
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
            { FirstName = user.FirstName, LastName = user.LastName, DateRegistration = (DateTime)user.RegistrationdDate, Image = user.Image };


            string profileName = Request.Cookies["profile"]?.Value;
            ProfileType profileType = (ProfileType)Enum.Parse(typeof(ProfileType), profileName);


            IEnumerable<FeedBack> profileFeedbacks = null;

            if(profileType == ProfileType.Employee)
                profileFeedbacks = _feedBackService.FindFeedBacksForEmployee(userId);
            else
                profileFeedbacks = _feedBackService.FindFeedBacksForCustomer(userId);

            int countFeedbacksForPer = profileFeedbacks.Count() == 0 ? 1 : profileFeedbacks.Count();
            double perPositiveFeedBacks = (double)profileFeedbacks.Count(p => (int)p.QualityOfWork >= 3) * 100 / countFeedbacksForPer;

            userInfo.CountFeedbacks = profileFeedbacks.Count();
            userInfo.PercentPositiveFeedbacks = (int)Math.Round(perPositiveFeedBacks);

            return PartialView(userInfo);
        }




        [HttpPost]
        public void SetProfileOnCustomer()
        {
            Response.Cookies["profile"].Value = Enum.GetName(typeof(ProfileType), ProfileType.Customer);
        }

        [HttpPost]
        public void SetProfileOnEmployee()
        {
            Response.Cookies["profile"].Value = Enum.GetName(typeof(ProfileType), ProfileType.Employee);
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