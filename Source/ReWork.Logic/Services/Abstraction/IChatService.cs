using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IChatService
    {
        void CreateChatRoom(string title, IEnumerable<string> usersId);
        void DeleteChatRoom(int id);
        void CreateMessage(string senderId, int chatRoomId, string text);

        IEnumerable<MessageInfo> FindMessages(int chatRoomId);
        void RefreshChatRoom(int chatRoomId);
    }
}
