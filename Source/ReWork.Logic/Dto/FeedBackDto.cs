using System;

namespace ReWork.Logic.Dto
{
    public class FeedBackDto
    {
        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        public QualityOfWorkDto QualityOfWork { get; set; }

        public JobDto Job { get; set; }
    }
}
