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


        [HttpPost]
        public ActionResult RecivedFeedBacks()
        {
            string userId = User.Identity.GetUserId();
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
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateRegistration = (DateTime)user.RegistrationdDate,
                Image = user.Image
            };

            IEnumerable<FeedBack> feedBacks = user.FeedBacks.ToList();

            int countFeedbacksForPer = feedBacks.Count() == 0 ? 1 : feedBacks.Count();
            double percentPositiveFeedBacks = (double)feedBacks.Count(p => (int)p.QualityOfWork >= 3) * 100 / countFeedbacksForPer;

            userInfo.CountFeedbacks = feedBacks.Count();
            userInfo.PercentPositiveFeedbacks = (int)Math.Round(percentPositiveFeedBacks);

            return PartialView(userInfo);
        }




        [HttpPost]
        public ActionResult ChangeProfileType(ProfileType profile)
        {
            HttpCookie profileCookie = Request.Cookies["profile"];

            profileCookie.Value = Enum.GetName(typeof(ProfileType), profile);
            profileCookie.Expires = DateTime.UtcNow.AddYears(1);
            Response.Cookies.Add(profileCookie);

            return Redirect(Request.UrlReferrer.PathAndQuery);
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