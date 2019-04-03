using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class EmployeeProfileRepository : IEmployeeProfileRepository
    {
        private ReWorkContext _db;
        public EmployeeProfileRepository(ReWorkContext db)
        {
            _db = db;
        }

        public void Create(EmployeeProfile item)
        {
            _db.EmployeeProfiles.Add(item);
        }

        public void Delete(EmployeeProfile item)
        {
            _db.EmployeeProfiles.Remove(item);
        }
 
        public IQueryable<EmployeeProfile> FindEmployes(Expression<Func<EmployeeProfile, Boolean>> predicate)
        {
            return _db.EmployeeProfiles.Where(predicate);
        }

        public EmployeeProfile GetEmployeeProfileById(string id)
        {
            return _db.EmployeeProfiles.SingleOrDefault(p => p.Id.Equals(id));
        }

        public void Update(EmployeeProfile item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
