using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:500, MinimumLength = 3)]
        public string Text { get; set; }

        [Required]
        public DateTime? AddedDate { get; set; }

        [Required]
        [Range(0, 10000)]
        public int? ImplementationDays { get; set; }

        [Required]
        [Range(0,1000000)]
        public int OfferPayment { get; set; }

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
