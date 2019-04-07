using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("Skills")]
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [ForeignKey("Section")]
        public int SectionId { get; set; }

        [Required]
        public virtual Section Section { get; set; }



        public virtual ICollection<EmployeeProfile> Employes { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public Skill()
        {
            Employes = new List<EmployeeProfile>();
            Jobs = new List<Job>();
        }
    }
}
