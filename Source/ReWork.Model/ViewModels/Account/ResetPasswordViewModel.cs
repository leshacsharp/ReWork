using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The UserName length must be at least 3 characters and no more than 20 characters", MinimumLength = 3)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The Passwordlength must be at least 6 characters and no more than 30 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
