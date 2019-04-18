using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class JobRepository : BaseRepository, IJobRepository
    { 
        public void Create(Job item)
        {
            Db.Jobs.Add(item);
        }

        public void Delete(Job item)
        {
            Db.Jobs.Remove(item);
        }

        public IQueryable<Job> FindJobs(Expression<Func<Job, bool>> predicate)
        {
            
            //return _db.Jobs.Where(predicate);
             return Db.Jobs.Include(p => p.Skills).Where(predicate);
        }

        public Job FindJobById(int id)
        {
            return Db.Jobs.Find(id);      
        }

        public void Update(Job item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
