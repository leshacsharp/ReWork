using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ICustomerProfileRepository : IRepository<CustomerProfile>
    {
        CustomerProfile FindCustomerProfileById(string customerId);
        CustomerProfileInfo FindCustomerProfileInfo(string customerId);
    }
}
