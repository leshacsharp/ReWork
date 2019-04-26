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
        EmployeeProfileInfo FindEmployeeInfoById(string employeeId);
        EmployeeProfile FindEmployeeById(string employeeId);
    }
}
