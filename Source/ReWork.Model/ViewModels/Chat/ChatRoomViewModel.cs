using System;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Chat
{
    public class ChatRoomViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? LastMessageDate { get; set; }

        public string LastMessage { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
