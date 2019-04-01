using ReWork.DataProvider.Context;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.Repositories.Abstraction;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public IEnumerable<EmployeeProfile> FindEmployesProfilesByAge(int age)
        {
            return _db.EmployeeProfiles.Where(p=>p.Age.Equals(age));
        }

        public void Update(EmployeeProfile item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
