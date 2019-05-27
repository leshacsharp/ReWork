using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Hubs.Abstraction
{
    public interface IChatHub
    {
        void Connect(int chatRoomId);
        void Disconnect(int chatRoomId);

        void RefreshChatRoom(int chatRoomId, string messagesJSON);
    }
}
