using ReWork.Model.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("EmployeeProfiles")]
    public class EmployeeProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Range(8,100)]
        public int Age { get; set; }

        [Required]
        [StringLength(700, MinimumLength = 10)]
        public string AboutMe { get; set; }

 

        public virtual ICollection<Job> DevelopingJobs { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<FeedBack> FeedBacks { get; set; }

        public EmployeeProfile()
        {
            DevelopingJobs = new List<Job>();
            Offers = new List<Offer>();
            Skills = new List<Skill>();
            FeedBacks = new List<FeedBack>();
        }
    }
}
