using ReWork.DataProvider.Entities.Common;
using ReWork.DataProvider.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.DataProvider.Entities
{
    [Table("EmployeeProfiles")]
    public class EmployeeProfile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        [Required]
        [Range(8,100)]
        public int Age { get; set; }

        [Required]
        public QualityOfWork Rating { get; set; }

        [Required]
        public virtual User User { get; set; }



        public virtual ICollection<Task> PerfomedTasks { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
