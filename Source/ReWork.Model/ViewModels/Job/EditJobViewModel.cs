using ReWork.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Job
{
    public class EditJobViewModel
    {
        public int JobId { get; set; }

        [Required]
        [StringLength(maximumLength: 70, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 3)]
        public string Description { get; set; }

        [Range(0, Int32.MaxValue)]
        public int Price { get; set; }

        public bool PriceDiscussed { get; set; }

        [CannotBeEmpty(ErrorMessage = "Select at least 1 skill")]
        public IEnumerable<int> SelectedSkills { get; set; }
    }
}
