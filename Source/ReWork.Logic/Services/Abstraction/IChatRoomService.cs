using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IChatRoomService
    {
        void CreateChatRoom(IEnumerable<string> usersId);
        void AddMemberToChatRoom(int chatRoomId, string userId);
        void DeleteMemberFromChatRoom(int chatRoomId, string userId);

        ChatRoomDetailsInfo FindChatRoom(int id);
        IEnumerable<ChatRoomInfo> FindChatRooms(string userId);

        void RefreshChatRoom(int chatRoomId);
    }
}
