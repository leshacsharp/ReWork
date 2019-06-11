using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Job
{
    public class JobsViewModel
    {
        public int[] SkillsId { get; set; }

        [Range(0,1000000, ErrorMessage = "min price 0, max price 1000000")]
        [Display(Name = "Price from")]
        public int PriceFrom { get; set; }

        [MaxLength(60,ErrorMessage = "max length is 60")]
        [Display(Name = "Key words")]
        public string KeyWords { get; set; }
    }
}
