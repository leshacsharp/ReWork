using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        ChatRoom FindById(int id);
    }
}
