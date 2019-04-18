using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Model.ViewModels.Job
{
    public class DetailsJobViewModel
    {
        public Entities.Job Job { get; set; }
        public IEnumerable<OfferInfo> Offers { get; set; }
    }
}
