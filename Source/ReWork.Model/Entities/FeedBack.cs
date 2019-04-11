using ReWork.Model.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("FeedBacks")]
    public class FeedBack
    {
        [Key]
        [ForeignKey("Job")]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3)]
        public string Text { get; set; }

        [Required]
        public DateTime? AddedDate { get; set; }

        public QualityOfWork QualityOfWork { get; set; }

        [Required]
        public virtual Job Job { get; set; }
    }
}
