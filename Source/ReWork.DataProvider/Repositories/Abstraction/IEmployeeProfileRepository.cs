using ReWork.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.DataProvider.Repositories.Abstraction
{
    public interface IEmployeeProfileRepository : IRepository<EmployeeProfile>
    {
        IEnumerable<EmployeeProfile> FindEmployesProfilesByAge(int age);
    }
}
