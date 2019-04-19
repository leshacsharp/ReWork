using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Job
{
    public class DetailsJobViewModel
    {
        public JobInfo Job { get; set; }
        public IEnumerable<OfferInfo> Offers { get; set; }
    }
}
