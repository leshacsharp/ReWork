using ReWork.Logic.Services.Params;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IOfferService
    {
        void CreateOffer(CreateOfferParams offerParams);
        IEnumerable<OfferInfo> FindJobOffers(int jobId);
    }
}
