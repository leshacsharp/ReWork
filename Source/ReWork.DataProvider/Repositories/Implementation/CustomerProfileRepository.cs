using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class CustomerProfileRepository : BaseRepository, ICustomerProfileRepository
    {
        public void Create(CustomerProfile item)
        {
            Db.CustomerProfiles.Add(item);
        }

        public void Delete(CustomerProfile item)
        {
            Db.CustomerProfiles.Remove(item);
        }

        public CustomerProfile FindCustomerProfileById(string customerId)
        {
            return Db.CustomerProfiles.Find(customerId);
        }

        public CustomerProfile FindCustomerProfileByName(string userName)
        {
            return Db.CustomerProfiles.SingleOrDefault(p => p.User.UserName == userName);
        }

        public void Update(CustomerProfile item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
