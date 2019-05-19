using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Offer FindOfferById(int offerId);
        Offer FindOffer(int jobId, string employeeId);

        IEnumerable<OfferInfo> FindJobOffers(int jobId);
        IEnumerable<CustomerOfferInfo> FindCustomerOffers(string customerId);
        IEnumerable<EmployeeOfferInfo> FindEmployeeOffers(string employeeId);
    }
}
