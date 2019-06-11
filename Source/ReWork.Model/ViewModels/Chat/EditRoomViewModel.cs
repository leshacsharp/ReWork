using System.ComponentModel.DataAnnotations;

namespace ReWork.Model.ViewModels.Chat
{
    public class EditRoomViewModel
    {
        public int ChatRoomId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "New title")]
        public string NewTitle { get; set; }
    }
}
