using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Employee
{
    public class EmployeeDetailsViewModel
    {
        public string Id { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public int Age { get; set; }

        [Display(Name = "About me")]
        public string AboutMe { get; set; }

        [Display(Name = "Registrationd date")]
        public DateTime RegistrationdDate { get; set; }

        [Display(Name = "Count devoloping jobs")]
        public int CountDevolopingJobs { get; set; }

        [Display(Name = "Count reviews")]
        public int CountReviews { get; set; }

        public int PercentPositiveReviews { get; set; }

        public int AvarageReviewMark { get; set; }


        public IEnumerable<string> Skills { get; set; }
    }
}
