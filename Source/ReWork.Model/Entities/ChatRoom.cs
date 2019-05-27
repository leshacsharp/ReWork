using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReWork.Model.Entities
{
    [Table("ChatRooms")]
    public class ChatRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 5)]
        public string Title { get; set; }

        public virtual ICollection<Message> Messages{ get; set; }

        public virtual ICollection<User> Users { get; set; }

        public ChatRoom()
        {
            Messages = new List<Message>();
            Users = new List<User>();
        }
    }
}
