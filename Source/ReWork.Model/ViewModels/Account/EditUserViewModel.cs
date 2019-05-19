using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Account
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]  
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        public string ImagePath { get; set; }
    }
}
