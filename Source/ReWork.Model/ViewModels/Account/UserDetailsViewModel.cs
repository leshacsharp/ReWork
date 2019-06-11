using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Account
{
    public class UserDetailsViewModel
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
