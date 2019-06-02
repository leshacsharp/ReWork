using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class ChatRoomDetailsInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<ChatRoomUsersInfo> Users { get; set; }
    }
}
