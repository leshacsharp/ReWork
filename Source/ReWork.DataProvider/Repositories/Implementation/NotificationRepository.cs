using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public void Create(Notification item)
        {
            Db.Notifications.Add(item);
        }

        public void Delete(Notification item)
        {
            Db.Notifications.Remove(item);
        }

        public Notification FindById(int notificationId)
        {
            return Db.Notifications.Find(notificationId);
        }

        public IEnumerable<NotificationInfo> FindNotificationsInfo(string userId)
        {
            return (from n in Db.Notifications
                    join s in Db.Users on n.SenderId equals s.Id
                    select new NotificationInfo()
                    {
                        Id = n.Id,
                        Text = n.Text,
                        AddedDate = (DateTime)n.AddedDate,
                        SenderId = s.Id,
                        SenderName = s.UserName,
                        SenderImage = s.Image
                    }).ToList();
        }

        public void Update(Notification item)
        {
            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
