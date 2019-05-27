using Microsoft.AspNet.SignalR;
using ReWork.Logic.Hubs.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Hubs.Implementation
{
    public class ChatHub : Hub, IChatHub
    {
        public void Connect(int chatRoomId)
        {
            Groups.Add(Context.ConnectionId, chatRoomId.ToString());
        }

        public void Disconnect(int chatRoomId)
        {
            Groups.Remove(Context.ConnectionId, chatRoomId.ToString());
        }

        public void RefreshChatRoom(int chatRoomId, string messagesJSON)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
            context.Clients.Group(chatRoomId.ToString()).refreshChatRoom(messagesJSON);
        }
    }
}
