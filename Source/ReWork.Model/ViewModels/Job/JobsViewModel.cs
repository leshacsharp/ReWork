using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Job
{
    public class JobsViewModel
    {
        public int[] SkillsId { get; set; }

        [Range(0,1000000, ErrorMessage = "min price 0, max price 1000000")]
        public int PriceFrom { get; set; }

        [MaxLength(60,ErrorMessage = "max length is 60")]
        public string KeyWords { get; set; }
    }
}
