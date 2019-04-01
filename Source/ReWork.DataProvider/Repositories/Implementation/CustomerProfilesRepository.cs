using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class CustomerProfilesRepository : ICustomerProfileRepository
    {
        private ReWorkContext _db;
        public CustomerProfilesRepository(ReWorkContext db)
        {
            _db = db;
        }

        public void Create(CustomerProfile item)
        {
            _db.CustomerProfiles.Add(item);
        }

        public void Delete(CustomerProfile item)
        {
            _db.CustomerProfiles.Remove(item);
        }

        public IEnumerable<CustomerProfile> FindCustomerProfilesByName(string userName)
        {
            return _db.CustomerProfiles.Where(p => p.User.UserName.Equals(userName));
        }

        public void Update(CustomerProfile item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
