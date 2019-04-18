using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<Offer> FindOffersByUserId(string userId);
        IEnumerable<OfferInfo> FindJobOffers(int jobId);
    }
}
