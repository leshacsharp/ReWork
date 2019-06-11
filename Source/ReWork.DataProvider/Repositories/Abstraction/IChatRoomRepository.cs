using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        ChatRoom FindById(int id);
        ChatRoomDetailsInfo FindChatRoomInfo(int id);
        IEnumerable<ChatRoomInfo> FindChatRooms(string userId);
    }
}
