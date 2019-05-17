using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification FindById(int notificationId);
        IEnumerable<NotificationInfo> FindNotificationsInfo(string userId);
    }
}
