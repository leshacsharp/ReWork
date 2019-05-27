using Microsoft.AspNet.SignalR;
using ReWork.Logic.Hubs.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Linq;

namespace ReWork.Logic.Hubs.Implementation
{
    public class NotificationHub : Hub, INotificationHub
    {
        public override Task OnConnected()
        {
            var user = Context.User;
            if (user.Identity.IsAuthenticated)
            {
                string userId = user.Identity.GetUserId();
                Groups.Add(Context.ConnectionId, userId);
            }
            
            return base.OnConnected();
        }


        public void RefreshNotifications(string userId, string notificationsJson)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.Group(userId).refreshNotifications(notificationsJson);
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var user = Context.User;
            if (user.Identity.IsAuthenticated)
            {
                string userId = user.Identity.GetUserId();
                Groups.Remove(Context.ConnectionId, userId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
