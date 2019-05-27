using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using ReWork.Logic.Hubs.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Hubs.Implementation
{
    public class UserHub : Hub, IUserHub
    {
        private static HubConnections<string> _connections;

        static UserHub()
        {
            _connections = new HubConnections<string>();
        }

        public override Task OnConnected()
        {
            var user = Context.User;
            if(user.Identity.IsAuthenticated)
            {
                string userId = user.Identity.GetUserId();
                _connections.Add(userId, Context.ConnectionId);
            }

            return base.OnConnected();
        }


        public void RefreshUsersCounter()
        {
            int onlineUsersCount = _connections.Count;
            Clients.All.refreshUsersCounter(onlineUsersCount);
        }


        public void IsOnline(string userId)
        {
            var connections = _connections.FindConnections(userId);
            bool isOnline = connections == null ? false : true; 

            Clients.Caller.userIsOnline(isOnline);
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
