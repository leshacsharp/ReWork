using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IJobRepository : IRepository<Job>
    {
        IQueryable<JobInfo> FindJobsInfo(Expression<Func<Job, Boolean>> predicate);
        MyJobInfo FindMyJobInfo(int id);
        Job FindJobById(int id);
    }
}
