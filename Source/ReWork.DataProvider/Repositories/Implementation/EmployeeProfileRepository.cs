using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class EmployeeProfileRepository : BaseRepository, IEmployeeProfileRepository
    {
        public void Create(EmployeeProfile item)
        {
            Db.EmployeeProfiles.Add(item);
        }

        public void Delete(EmployeeProfile item)
        {
            Db.EmployeeProfiles.Remove(item);
        }
 
        public IQueryable<EmployeeProfile> FindEmployes(Expression<Func<EmployeeProfile, Boolean>> predicate)
        {
            return Db.EmployeeProfiles.Where(predicate);
        }

        public EmployeeProfile FindEmployeeProfileById(string employeeId)
        {
            return Db.EmployeeProfiles.Find(employeeId);
        }

        public void Update(EmployeeProfile item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
