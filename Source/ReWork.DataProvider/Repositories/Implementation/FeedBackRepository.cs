using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private ReWorkContext _db;
        public FeedBackRepository(ReWorkContext db)
        {
            _db = db;
        }

        public void Create(FeedBack item)
        {
            _db.FeedBacks.Add(item);
        }

        public void Delete(FeedBack item)
        {
            _db.FeedBacks.Remove(item);
        }

        public IEnumerable<FeedBack> FindFeedBacksByUserName(string userName)
        {
            return _db.FeedBacks.Where(p => p.Job.Customer.User.UserName.Equals(userName));
        }

        public void Update(FeedBack item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
