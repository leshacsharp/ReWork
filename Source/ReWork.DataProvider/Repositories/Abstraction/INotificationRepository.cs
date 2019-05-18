using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification FindById(int notificationId);
        void DeleteAll(string userId);
        IEnumerable<NotificationInfo> FindNotificationsInfo(string userId);
    }
}
