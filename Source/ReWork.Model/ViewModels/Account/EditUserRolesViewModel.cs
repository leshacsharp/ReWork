using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Account
{
    public class EditUserRolesViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        public IEnumerable<string> AllRoles { get; set; }

        [Display(Name = "User roles")]
        public IEnumerable<string> UserRoles { get; set; }
    }
}
