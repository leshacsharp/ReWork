using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IMessageService
    {
        void CreateMessage(string senderId, int chatRoomId, string text);
        IEnumerable<MessageInfo> FindMessages(int chatRoomId, int page, int count);
    }
}
