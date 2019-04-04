using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class JobRepository : IJobRepository
    {
        private IDbContext _db;
        public JobRepository(IDbContext db)
        {
            _db = db;
        }

        public void Create(Job item)
        {
            _db.Jobs.Add(item);
        }

        public void Delete(Job item)
        {
            _db.Jobs.Remove(item);
        }

        public IQueryable<Job> FindJobs(Expression<Func<Job, bool>> predicate)
        {
            return _db.Jobs.Where(predicate);   
        }

        public Job GetJobById(int id)
        {
            return _db.Jobs.SingleOrDefault(p=>p.Id.Equals(id));
        }

        public void Update(Job item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
