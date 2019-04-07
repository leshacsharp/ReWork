using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("Sections")]
    public class Section
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public Section()
        {
            Skills = new List<Skill>();
        }
    }
}
