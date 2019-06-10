using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;
using System.Linq;

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

        public ChatRoomDetailsInfo FindChatRoomInfo(int id)
        {
            return (from ch in Db.ChatRooms
                    where ch.Id == id
                    select new ChatRoomDetailsInfo()
                    {
                        Id = ch.Id,
                        Title = ch.Title,
                        Users = (from u in ch.Users
                                 select new ChatRoomUsersInfo()
                                 {
                                     Id = u.Id,
                                     UserName = u.UserName,
                                     Image = u.Image
                                 })
                    }).SingleOrDefault();
        }

        public IEnumerable<ChatRoomInfo> FindChatRooms(string userId)
        {
            return (from ch in Db.ChatRooms
                    where ch.Users.Any(p => p.Id == userId)
                    select new ChatRoomInfo()
                    {
                        Id = ch.Id,
                        Title = ch.Title,

                        LastMessage = (from m in ch.Messages
                                       orderby m.DateAdded descending
                                       select new ChatRoomMsgInfo()
                                       {
                                           DateAdded = m.DateAdded,
                                           Text = m.Text
                                       }).FirstOrDefault(),

                        Users = (from u in ch.Users
                                 select new ChatRoomUsersInfo()
                                 {
                                     Id = u.Id,
                                     UserName = u.UserName,
                                     Image = u.Image
                                 })
                    }).OrderByDescending(p => p.LastMessage.DateAdded).ToList();
                       
        }

        public void Update(ChatRoom item)
        {
            Db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
