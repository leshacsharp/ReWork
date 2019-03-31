using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.DataProvider.Entities
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:250, MinimumLength = 3)]
        public string Text { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        [Required]
        public TimeSpan ImplementationTime { get; set; }

        [Required]
        [Range(0,Int32.MaxValue)]
        public TimeSpan OfferPayment { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }

        [Required]
        public virtual Task Task { get; set; }

        [ForeignKey("Employee")]
        public int EpmployeeId { get; set; }

        public virtual EmployeeProfile Employee { get; set; }
    }
}
