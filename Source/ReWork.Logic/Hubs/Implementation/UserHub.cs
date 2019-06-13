using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using ReWork.Logic.Hubs.Abstraction;
using ReWork.Model.Entities.Common;
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

            RefreshUsersCounter();

            return base.OnConnected();
        }


        public void CheckUserStatus(string userId)
        {
            var connections = _connections.FindConnections(userId);
            UserStatus status = connections == null ? UserStatus.Offline : UserStatus.Online; 

            Clients.Caller.checkStatus(status);
        }


        public override Task OnDisconnected(bool stopCalled)
        {
            var user = Context.User;
            if (user.Identity.IsAuthenticated)
            {
                string userId = user.Identity.GetUserId();
                _connections.Remove(userId, Context.ConnectionId);
            }

            RefreshUsersCounter();

            return base.OnDisconnected(stopCalled);
        }

        private void RefreshUsersCounter()
        {
            int onlineUsersCount = _connections.Count;
            Clients.All.refreshUsersCounter(onlineUsersCount);
        }
    }
}
