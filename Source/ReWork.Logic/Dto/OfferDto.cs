using System;

namespace ReWork.Logic.Dto
{
    public class OfferDto
    {
        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        public TimeSpan ImplementationTime { get; set; }

        public TimeSpan OfferPayment { get; set; }


        public JobDto Job { get; set; }
        public EmployeeProfileDto Employee { get; set; }
    }
}
