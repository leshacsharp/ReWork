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
    
        [Range(8,100)]
        public int Age { get; set; }
 
        public QualityOfWork Rating { get; set; }

        [Required]
        public virtual User User { get; set; }



        public virtual ICollection<Job> PerfomedJobs { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public EmployeeProfile()
        {
            PerfomedJobs = new List<Job>();
            Offers = new List<Offer>();
            Skills = new List<Skill>();
        }
    }
}
