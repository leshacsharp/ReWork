using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Model.EntitiesInfo
{
    public class OfferInfo
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime? AddedDate { get; set; }

        public int? ImplementationDays { get; set; }

        public int OfferPayment { get; set; }

        public string EmployeeId { get; set; }
    }
}
