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

        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Required]
        public virtual Job Job { get; set; }

        [ForeignKey("Employee")]
        public string EpmployeeId { get; set; }

        [Required]
        public virtual EmployeeProfile Employee { get; set; }
    }
}
