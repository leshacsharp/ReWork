using System;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Offer
{
    public class CreateOfferViewModel
    {
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3)]
        public string Text { get; set; }

        [Required]
        public TimeSpan? ImplementationTime { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int OfferPayment { get; set; }
    }
}
