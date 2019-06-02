using System;

namespace ReWork.Model.ViewModels.Customer
{
    public class CustomerDetailsViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public DateTime RegistrationdDate { get; set; }

        public int CountPublishJobs { get; set; }

        public int CountReviews { get; set; }

        public int PercentPositiveReviews { get; set; }

        public int AvarageReviewMark { get; set; }
    }
}
