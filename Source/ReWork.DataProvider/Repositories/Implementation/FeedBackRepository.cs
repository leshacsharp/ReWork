using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System.Data.Entity;

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

        public void Update(FeedBack item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
