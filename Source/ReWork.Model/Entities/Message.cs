using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        public DateTime DateAdded { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }

        [Required]
        public virtual User Sender { get; set; }

        [ForeignKey("ChatRoom")]
        public int ChatRoomId { get; set; }

        [Required]
        public virtual ChatRoom ChatRoom { get; set; }
    }
}
