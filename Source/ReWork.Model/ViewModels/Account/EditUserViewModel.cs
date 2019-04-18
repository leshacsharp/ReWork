using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Model.ViewModels.Account
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public IEnumerable<string> AllRoles { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
