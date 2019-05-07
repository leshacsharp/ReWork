using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Model.EntitiesInfo
{
    public class CustomerOfferInfo
    {
        public string UserName { get; set; }

        public DateTime UserDateRegistration { get; set; }

        public string Text { get; set; }

        public DateTime AddedDate { get; set; }

        public int? ImplementationDays { get; set; }

        public int OfferPayment { get; set; }

        public byte[] EmployeeImage { get; set; }

        public string EmployeeId { get; set; }

        public string JobTitle { get; set; }

        public int JobId { get; set; }
    }
}
