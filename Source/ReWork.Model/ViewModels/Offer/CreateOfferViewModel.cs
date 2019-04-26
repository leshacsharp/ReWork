using System;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Offer
{
    public class CreateOfferViewModel
    {
        public int JobId { get; set; }

        public string EmployeeId { get; set; }

        [Required]
        [StringLength(maximumLength: 500, MinimumLength = 3)]
        public string Text { get; set; }

        [Range(1, 10000)]
        public int DaysToImplement { get; set; }

        [Range(0, 1000000)]
        public int OfferPayment { get; set; }
    }
}
