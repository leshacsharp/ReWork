using System;

namespace ReWork.Model.EntitiesInfo
{
    public class EmployeeOfferInfo
    {
        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        public int? ImplementationDays { get; set; }

        public int OfferPayment { get; set; }
  
        public int JobId { get; set; }

        public string JobTitle { get; set; }

        public int JobPrice { get; set; }

        public DateTime JobAdded { get; set; }
    }
}
