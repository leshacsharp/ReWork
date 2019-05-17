using System;

namespace ReWork.Model.ViewModels.Notification
{
    public class NotificationViewModel
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public string SenderImagePath { get; set; }

        public string Text { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
