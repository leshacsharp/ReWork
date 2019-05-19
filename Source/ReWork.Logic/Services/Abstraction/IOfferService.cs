using ReWork.Logic.Services.Params;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IOfferService
    {
        void CreateOffer(CreateOfferParams offerParams);     
        void AcceptOffer(int offerId, string employeeId);
        void RejectOffer(int offerId);

        IEnumerable<OfferInfo> FindJobOffers(int jobId);
        IEnumerable<EmployeeOfferInfo> FindEmployeeOffers(string employeeId);
        IEnumerable<CustomerOfferInfo> FindCustomerOffers(string customerId);

        bool OfferExists(int jobId, string employeeId);
    }
}
