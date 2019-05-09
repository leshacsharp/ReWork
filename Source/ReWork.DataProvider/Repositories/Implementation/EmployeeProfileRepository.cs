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
            return from e in Db.EmployeeProfiles.Where(predicate)
                   join u in Db.Users on e.Id equals u.Id
                   select new EmployeeProfileInfo()
                   {
                       Id = e.Id,
                       Age = e.Age,
                       AboutMe = e.AboutMe,
                       UserName = u.UserName,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       Image = u.Image,       
                       RegistrationdDate = (DateTime)u.RegistrationdDate,

                       CountDevolopingJobs = e.DevelopingJobs.Count,
                       QualityOfWorks = e.FeedBacks.Select(p => p.QualityOfWork),

                       Skills = e.Skills.Select(p => new SkillInfo()
                       {
                           Id = p.Id,
                           Title = p.Title
                       })
                   };
        }

        public EmployeeProfile FindEmployeeById(string employeeId)
        {
            return Db.EmployeeProfiles.Find(employeeId);
        }     

        public void Update(EmployeeProfile item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
