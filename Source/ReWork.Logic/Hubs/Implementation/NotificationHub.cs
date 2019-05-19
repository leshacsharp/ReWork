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
        private readonly static HubConnections<string> _connections;

        static NotificationHub()
        {
            _connections = new HubConnections<string>();
        }

        public override Task OnConnected()
        {
            var user = Context.User;
            if (user.Identity.IsAuthenticated)
            {
                string userId = user.Identity.GetUserId();
                _connections.Add(userId, Context.ConnectionId);
            }

            return base.OnConnected();
        }


        public void RefreshNotifications(string userId, string notificationsJson)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var connections = _connections.FindConnections(userId);

            if (connections != null)
            {
                foreach (var connectionId in connections)
                {
                    context.Clients.Client(connectionId).refreshNotifications(notificationsJson);
                }
            }
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var user = Context.User;
            if (user.Identity.IsAuthenticated)
            {
                string userId = user.Identity.GetUserId();
                _connections.Remove(userId, Context.ConnectionId);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
