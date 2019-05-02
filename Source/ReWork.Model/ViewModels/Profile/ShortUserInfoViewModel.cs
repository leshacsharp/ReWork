using System;

namespace ReWork.Model.ViewModels.Profile
{
    public class ShortUserInfoViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateRegistration { get; set; }

        public byte[] Image { get; set; }

        public int CountFeedbacks { get; set; }

        public int PercentPositiveFeedbacks { get; set; }
    }
}
