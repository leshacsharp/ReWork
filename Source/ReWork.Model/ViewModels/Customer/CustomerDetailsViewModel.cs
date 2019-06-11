using System;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Customer
{
    public class CustomerDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegistrationdDate { get; set; }

        [Display(Name = "Count publish jobs")]
        public int CountPublishJobs { get; set; }

        [Display(Name = "Count reviews")]
        public int CountReviews { get; set; }

        public int PercentPositiveReviews { get; set; }

        public int AvarageReviewMark { get; set; }
    }
}
