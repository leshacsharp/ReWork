using ReWork.Model.Entities.Common;
using System;

namespace ReWork.Model.EntitiesInfo
{
    public class FeedBackInfo
    {     
        public string Text { get; set; }

        public DateTime? AddedDate { get; set; }

        public QualityOfWork QualityOfWork { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public byte[] SenderImage { get; set; }

        public int JobId { get; set; }

        public string jobTitle { get; set; }
    }
}
