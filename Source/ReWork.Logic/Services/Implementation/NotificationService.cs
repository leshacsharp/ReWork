using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Hubs.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ReWork.Logic.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private INotificationRepository _notificationRepository;
        private UserManager<User> _userManager;
        private INotificationHub _hub;

        public NotificationService(INotificationRepository notificationRepository, INotificationHub hub, UserManager<User> userManager)
        {
            _notificationRepository = notificationRepository;
            _hub = hub;
            _userManager = userManager;
        }

        public void RefreshNotifications(string userId)
        {
            var notifications = _notificationRepository.FindNotificationsInfo(userId);

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
          

            string notificationsJson = JsonConvert.SerializeObject(notificationsViewModels);

            _hub.RefreshNotifications(userId, notificationsJson);
        }


        public void CreateNotification(string senderId, string reciverId, string text)
        {
            var sender = _userManager.FindById(senderId);
            if (sender == null)
                throw new ObjectNotFoundException($"User with id={senderId} not found");

            var reciver = _userManager.FindById(reciverId);
            if (reciver == null)
                throw new ObjectNotFoundException($"User with id={reciverId} not found");

            var notification = new Notification()
            {
                Sender = sender,
                Reciver = reciver,
                Text = text,
                AddedDate = DateTime.UtcNow              
            };

            _notificationRepository.Create(notification);
        }

        public void DeleteNotification(int id)
        {
            var notification = _notificationRepository.FindById(id);
            if(notification == null)
                throw new ObjectNotFoundException($"Notification with id={id} not found");

            _notificationRepository.Delete(notification);
        }

        public void DeleteAllNotifications(string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            _notificationRepository.DeleteAll(userId);
        }


        public IEnumerable<NotificationInfo> FindNotificationsInfo(string userId)
        {
            return _notificationRepository.FindNotificationsInfo(userId);
        }
    }
}
