using ReWork.Model.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IJobRepository : IRepository<Job>
    {
        IQueryable<Job> FindJobs(Expression<Func<Job, Boolean>> predicate);
        Job GetJobById(int id);
    }
}
