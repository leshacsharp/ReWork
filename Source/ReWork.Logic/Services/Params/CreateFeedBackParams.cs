using ReWork.Model.Entities.Common;

namespace ReWork.Logic.Services.Params
{
    public class CreateFeedBackParams
    {
        public string SenderId { get; set; }

        public string ReciverId { get; set; }

        public int JobId { get; set; }

        public string Text { get; set; }

        public QualityOfWork QualityOfWork { get; set; }
    }
}
