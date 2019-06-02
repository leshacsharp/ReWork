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
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3)]
        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        public QualityOfWork QualityOfWork { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [Required]
        public virtual User Sender { get; set; }

        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }

        [Required]
        public virtual User Receiver { get; set; }

        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Required]
        public virtual Job Job { get; set; }
    }
}
