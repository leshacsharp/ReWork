using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Context;
using ReWork.Model.Entities.Common;
using ReWork.Model.ViewModels.FeedBack;
using ReWork.Model.ViewModels.Profile;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUserService _userService;
        private INotificationService _notificationService;
        private IFeedBackService _feedBackService;
        private ICommitProvider _commitProvider;

        public ProfileController(IUserService userService, INotificationService notificationService, IFeedBackService feedBackService, ICommitProvider commitProvider)
        {
            _userService = userService;
            _notificationService = notificationService;
            _feedBackService = feedBackService;
            _commitProvider = commitProvider;
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
                QualityOfWork = (QualityOfWork)createModel.QualityOfWork
            };

            _feedBackService.CreateFeedBack(createParams);
            _notificationService.CreateNotification(senderId, createModel.ReciverId, "You write a review");
            _commitProvider.SaveChanges();

            _notificationService.RefreshNotifications(createModel.ReciverId);

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
            var feedbacks = _feedBackService.FindRecivedFeedBacks(userId);

            var feedbackModels = from f in feedbacks
                                 select new FeedBackViewModel()
                                 {
                                     Text = f.Text,
                                     AddedDate = f.AddedDate,
                                     JobId = f.JobId,
                                     JobTitle = f.JobTitle,
                                     SenderId = f.SenderId,
                                     SenderName = f.SenderName,
                                     SenderImagePath = Convert.ToBase64String(f.SenderImage),
                                     QualityOfWork = f.QualityOfWork
                                 };

            return Json(feedbackModels);
        }


        [HttpGet]
        public ActionResult Information()
        {
            string userId = User.Identity.GetUserId();
            var user = _userService.FindUserById(userId);
            return View(user);
        }

        public ActionResult UserInformation()
        {
            string userId = User.Identity.GetUserId();
            var user = _userService.FindUserById(userId);

            var userInfo = new ShortUserInfoViewModel()
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