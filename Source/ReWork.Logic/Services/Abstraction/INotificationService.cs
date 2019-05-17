using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface INotificationService
    {      
        void CreateNotification(string senderId, string reciverId, string text);
        void DeleteNotification(int id);

        IEnumerable<NotificationInfo> FindNotificationsInfo(string userId);
        void RefreshNotifications(string userId);
    }
}
