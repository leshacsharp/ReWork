using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private INotificationService _notificationService;
        private ICommitProvider _commitProvider;

        public NotificationController(INotificationService notificationService, ICommitProvider commitProvider )
        {
            _notificationService = notificationService;
            _commitProvider = commitProvider;
        }
  
        [HttpPost]
        public void CreateNotify(string reciverId, string text)
        {
            string senderId = User.Identity.GetUserId();
            _notificationService.CreateNotification(senderId, reciverId, text);
            _commitProvider.SaveChanges();

            _notificationService.RefreshNotifications(reciverId);
        }

        [HttpPost]
        public void Delete(int notifyId)
        {
            _notificationService.DeleteNotification(notifyId);
            _commitProvider.SaveChanges();
        }

        [HttpPost]
        public void DeleteAll()
        {
            string userId = User.Identity.GetUserId();

            _notificationService.DeleteAllNotifications(userId);
            _commitProvider.SaveChanges();
        }


        [HttpPost]
        public ActionResult FindNotifications()
        {
            string userId = User.Identity.GetUserId();
            var notifications = _notificationService.FindNotificationsInfo(userId);

            var notificationsViewModels = from n in notifications
                                          select new NotificationViewModel()
                                          {
                                              Id = n.Id,
                                              Text = n.Text,
                                              AddedDate = n.AddedDate,
                                              SenderId = n.SenderId,
                                              SenderName = n.SenderName,
                                              SenderImagePath = Convert.ToBase64String(n.SenderImage)
                                          };

            return Json(notificationsViewModels);
        }
    }
}