using ReWork.Model.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Account
{
    public class EmployeeProfileViewModel
    {
        [Required]
        [Range(10, 100, ErrorMessage = "Age must be from 10 to 100")]
        public int Age { get; set; }
        
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
