using System;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Employee
{
    public class EmployeeDetailsViewModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Image { get; set; }

        public int Age { get; set; }

        public string AboutMe { get; set; }

        public DateTime RegistrationdDate { get; set; }

        public int CountDevolopingJobs { get; set; }


        public int CountReviews { get; set; }

        public int PercentPositiveReviews { get; set; }

        public int AvarageReviewMark { get; set; }


        public IEnumerable<string> Skills { get; set; }
    }
}
