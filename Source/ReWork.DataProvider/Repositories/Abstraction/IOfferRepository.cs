using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IOfferRepository : IRepository<Offer>
    {
        IEnumerable<OfferInfo> FindJobOffers(int jobId);
        IEnumerable<OfferInfo> FindCustomerOffers(string customerId);
        IEnumerable<OfferInfo> FindEmployeeOffers(string employeeId);
    }
}
