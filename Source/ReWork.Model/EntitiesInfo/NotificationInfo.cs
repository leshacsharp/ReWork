using System;

namespace ReWork.Model.EntitiesInfo
{
    public class NotificationInfo
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public byte[] SenderImage { get; set; }

        public string Text { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
