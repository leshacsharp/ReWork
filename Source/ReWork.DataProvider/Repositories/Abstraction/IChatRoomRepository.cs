using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        ChatRoom FindById(int id);
        ChatRoomDetailsInfo FindChatRoomInfo(int id);
        IEnumerable<ChatRoomInfo> FindChatRooms(string userId);
    }
}
