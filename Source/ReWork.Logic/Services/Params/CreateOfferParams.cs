using System;

namespace ReWork.Logic.Services.Params
{
    public class CreateOfferParams
    {
        public string EmployeeId { get; set; }

        public int JobId { get; set; }

        public string Text { get; set; }

        public int ImplementationDays { get; set; }

        public int OfferPayment { get; set; }
    }
}
