using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IEmployeeProfileRepository : IRepository<EmployeeProfile>
    {
        IQueryable<EmployeeProfileInfo> FindEmployes(Expression<Func<EmployeeProfile, Boolean>> predicate);
        EmployeeProfile FindEmployeeById(string employeeId);
    }
}
