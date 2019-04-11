using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private IDbContext _db;
        public CustomerProfileRepository(IDbContext db)
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

        public CustomerProfile FindCustomerProfileById(string customerId)
        {
            return _db.CustomerProfiles.Find(customerId);
        }

        public CustomerProfile FindCustomerProfileByName(string userName)
        {
            return _db.CustomerProfiles.SingleOrDefault(p => p.User.UserName.Equals(userName));
        }

        public void Update(CustomerProfile item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
