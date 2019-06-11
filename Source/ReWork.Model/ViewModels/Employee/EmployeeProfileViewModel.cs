using ReWork.Model.Validation;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Employee
{
    public class EmployeeProfileViewModel
    {
        [Required]
        [Range(10, 100, ErrorMessage = "Age must be from 10 to 100")]
        public int Age { get; set; }

        [Required]
        [StringLength(700, MinimumLength = 10)]
        [Display(Name = "About me")]
        public string AboutMe { get; set; }

        [CannotBeEmpty(ErrorMessage = "Select at least 1 skill")]
        [Display(Name = "Selected skills")]
        public int[] SelectedSkills { get; set; }
    }
}
