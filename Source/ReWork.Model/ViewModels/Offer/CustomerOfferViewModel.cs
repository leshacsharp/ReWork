using System;

namespace ReWork.Model.ViewModels.Offer
{
    public class CustomerOfferViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime UserDateRegistration { get; set; }

        public DateTime AddedDate { get; set; }

        public int? ImplementationDays { get; set; }

        public int OfferPayment { get; set; }

        public string EmployeeId { get; set; }

        public string EmployeeImagePath { get; set; }

        public int JobId { get; set; }

        public string JobTitle { get; set; }
    }
}
