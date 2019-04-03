using ReWork.DataProvider.Entities;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ICustomerProfileRepository : IRepository<CustomerProfile>
    {
        CustomerProfile FindCustomerProfileByName(string userName);
    }
}
