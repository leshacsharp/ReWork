using ReWork.DataProvider.Entities;
using System.Collections.Generic;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface ICustomerProfileRepository : IRepository<CustomerProfile>
    {
        IEnumerable<CustomerProfile> FindCustomerProfilesByName(string userName);
    
    }
}
