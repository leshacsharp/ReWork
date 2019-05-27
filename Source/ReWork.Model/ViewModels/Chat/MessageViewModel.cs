using System;

namespace ReWork.Model.ViewModels.Chat
{
    public class MessageViewModel
    {
        public string Text { get; set; }

        public DateTime DateAdded { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public string SenderImagePath { get; set; }
    }
}
