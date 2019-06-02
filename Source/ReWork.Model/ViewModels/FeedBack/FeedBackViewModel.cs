using ReWork.Model.Entities.Common;
using System;

namespace ReWork.Model.ViewModels.FeedBack
{
    public class FeedBackViewModel
    {
        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        public QualityOfWork QualityOfWork { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public string SenderImagePath { get; set; }

        public int JobId { get; set; }

        public string JobTitle { get; set; }
    }
}
