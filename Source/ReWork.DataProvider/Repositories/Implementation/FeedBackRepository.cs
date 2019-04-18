using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class FeedBackRepository : BaseRepository, IFeedBackRepository
    {
        public void Create(FeedBack item)
        {
            Db.FeedBacks.Add(item);
        }

        public void Delete(FeedBack item)
        {
            Db.FeedBacks.Remove(item);
        }

        public IEnumerable<FeedBack> FindFeedBacksByUserName(string userName)
        {
            return Db.FeedBacks.Where(p => p.Job.Customer.User.UserName == userName).ToList();
        }

        public void Update(FeedBack item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
