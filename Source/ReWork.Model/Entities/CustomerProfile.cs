using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("CustomerProfiles")]
    public class CustomerProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<FeedBack> FeedBacks { get; set; }

        public CustomerProfile()
        {
            Jobs = new List<Job>();
            FeedBacks = new List<FeedBack>();
        }
    }
}
