using ReWork.DataProvider.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IEmployeeProfileRepository : IRepository<EmployeeProfile>
    {
        IQueryable<EmployeeProfile> FindEmployes(Expression<Func<EmployeeProfile, Boolean>> predicate);
        EmployeeProfile GetEmployeeProfileById(string id);
    }
}
