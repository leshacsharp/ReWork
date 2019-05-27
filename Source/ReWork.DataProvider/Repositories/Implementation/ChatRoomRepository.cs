using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class ChatRoomRepository : BaseRepository, IChatRoomRepository
    {
        public void Create(ChatRoom item)
        {
            Db.ChatRooms.Add(item);
        }

        public void Delete(ChatRoom item)
        {
            Db.ChatRooms.Remove(item);
        }

        public ChatRoom FindById(int id)
        {
            return Db.ChatRooms.Find(id);
        }

        public void Update(ChatRoom item)
        {
            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
