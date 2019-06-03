using ReWork.Model.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.FeedBack
{
    public class CreateFeedBackViewModel
    {
        [Required]
        public string ReciverId { get; set; }

        public int JobId { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Text { get; set; }

        [Range(1,5)]
        public int QualityOfWork { get; set; }
    }
}
