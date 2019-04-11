using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private IDbContext _db;
        public FeedBackRepository(IDbContext db)
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
            return _db.FeedBacks.Where(p => p.Job.Customer.User.UserName.Equals(userName)).ToList();
        }

        public void Update(FeedBack item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
