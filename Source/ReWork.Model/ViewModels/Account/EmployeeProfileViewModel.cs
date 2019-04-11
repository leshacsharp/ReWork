using ReWork.Model.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReWork.Model.ViewModels.Account
{
    public class EmployeeProfileViewModel
    {
        [Required]
        [Range(10, 100, ErrorMessage = "Age must be from 10 to 100")]
        public int Age { get; set; }

        [CannotBeEmpty(ErrorMessage = "Select at least 1 skill")]
        public IEnumerable<int> SelectedSkills { get; set; }

        public IEnumerable<SelectListItem> Skills { get; set; }
    }
}
