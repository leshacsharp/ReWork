using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Chat
{
    public class ChatRoomDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
