using System;

namespace ReWork.Model.EntitiesInfo
{
    public class MessageInfo
    {
        public string Text { get; set; }

        public DateTime DateAdded { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public byte[] SenderImage { get; set; }
    }
}
