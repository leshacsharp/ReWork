using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class OfferRepository : IOfferRepository
    {
        private IDbContext _db;
        public OfferRepository(IDbContext db)
        {
            _db = db;
        }

        public void Create(Offer item)
        {
            _db.Offers.Add(item);
        }

        public void Delete(Offer item)
        {
            _db.Offers.Remove(item);
        }

        public IEnumerable<Offer> FindOffersByUserName(string userName)
        {
            return _db.Offers.Where(p => p.Employee.User.UserName.Equals(userName)).ToList();
        }

        public void Update(Offer item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
