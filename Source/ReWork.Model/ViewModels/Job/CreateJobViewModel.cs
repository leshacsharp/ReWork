using ReWork.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ReWork.Model.ViewModels.Job
{
    public class CreateJobViewModel
    {
        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 500, MinimumLength = 10)]
        public string Description { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }

        [CannotBeEmpty(ErrorMessage = "Select at least 1 skill")]
        public IEnumerable<int> SelectedSkills { get; set; }

        public IEnumerable<SelectListItem> Skills { get; set; }
    }
}
