using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250,MinimumLength = 10)]
        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [Required]
        public User Sender { get; set; }

        [ForeignKey("Reciver")]
        public string ReciverId { get; set; }

        [Required]
        public User Reciver { get; set; }
    }
}
