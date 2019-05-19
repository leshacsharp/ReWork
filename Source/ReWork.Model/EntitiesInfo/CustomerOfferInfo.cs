using System;

namespace ReWork.Model.EntitiesInfo
{
    public class CustomerOfferInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime UserDateRegistration { get; set; }

        public DateTime AddedDate { get; set; }

        public int? ImplementationDays { get; set; }

        public int OfferPayment { get; set; }

        public string EmployeeId { get; set; }

        public byte[] EmployeeImage { get; set; }

        public int JobId { get; set; }

        public string JobTitle { get; set; }
    }
}
