using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public void Create(Message item)
        {
            Db.Messages.Add(item);
        }

        public void Delete(Message item)
        {
            Db.Messages.Remove(item);
        }

        public IQueryable<MessageInfo> FindMessageInfo(int chatRoomId)
        {
            return from m in Db.Messages
                   join u in Db.Users on m.SenderId equals u.Id
                   orderby m.DateAdded descending
                   where m.ChatRoomId == chatRoomId
                   select new MessageInfo()
                   {
                       Text = m.Text,
                       DateAdded = m.DateAdded,

                       SenderId = u.Id,
                       SenderName = u.UserName,
                       SenderImage = u.Image
                   };
        }

        public void Update(Message item)
        {
            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
