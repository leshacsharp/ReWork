using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
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

        public IQueryable<EmployeeProfileInfo> FindEmployes(Expression<Func<EmployeeProfile, Boolean>> predicate)
        {
            return Db.EmployeeProfiles
                     .Where(predicate)
                     .Select(e => new EmployeeProfileInfo()
                     {
                         Id = e.Id,
                         UserName = e.User.UserName,
                         FirstName = e.User.FirstName,
                         LastName = e.User.LastName,
                         Rating = e.Rating,
                         Age = e.Age,
                         RegistrationdDate = (DateTime)e.User.RegistrationdDate,
                         CountPerfomedJobs = e.PerfomedJobs.Count,

                         Skills = e.Skills.Select(p => new SkillInfo()
                         {
                             Id = p.Id,
                             Title = p.Title
                         })
                     });
        }

        public EmployeeProfile FindEmployeeById(string employeeId)
        {
            return Db.EmployeeProfiles.Find(employeeId);
        }

        public EmployeeProfileInfo FindEmployeeInfoById(string employeeId)
        {
            return (from e in Db.EmployeeProfiles
                    join u in Db.Users on e.Id equals u.Id
                    where e.Id == employeeId
                    select new EmployeeProfileInfo()
                    {
                        Id = e.Id,
                        UserName = e.User.UserName,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        Rating = e.Rating,
                        Age = e.Age,
                        RegistrationdDate = (DateTime)u.RegistrationdDate,
                        CountPerfomedJobs = e.PerfomedJobs.Count,

                        Skills = e.Skills.Select(p => new SkillInfo()
                        {
                            Id = p.Id,
                            Title = p.Title
                        })
                    }).SingleOrDefault();
        }

        public void Update(EmployeeProfile item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
