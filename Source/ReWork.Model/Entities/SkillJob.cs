using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("SkillJobs")]
    public class SkillJob
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Job")]
        public int JobId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Skill")]
        public int SkillId { get; set; }

        public virtual Job Job { get; set; }

        public virtual Skill Skill { get; set; }
    }
}
