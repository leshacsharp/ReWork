using System;
using System.Collections.Generic;

namespace ReWork.Model.EntitiesInfo
{
    public class ChatRoomInfo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ChatRoomMsgInfo LastMessage { get; set; }

        public IEnumerable<ChatRoomUsersInfo> Users { get; set; }
    }
}
