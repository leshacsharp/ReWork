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
                         Image = e.User.Image,
                         Age = e.Age,
                         RegistrationdDate = (DateTime)e.User.RegistrationdDate,

                         CountDevolopingJobs = e.DevelopingJobs.Count,
                         QualityOfWorks = e.FeedBacks.Select(p=>p.QualityOfWork),

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
                    join f in Db.FeedBacks on e.Id equals f.EmployeeProfileId into feedBacks
                    join j in Db.Jobs on e.Id equals j.EmployeeId into jobs
                    where e.Id == employeeId
                    select new EmployeeProfileInfo()
                    {
                        Id = e.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        AboutMe = e.AboutMe,
                        Image = u.Image,
                        Age = e.Age,
                        RegistrationdDate = (DateTime)u.RegistrationdDate,

                        CountDevolopingJobs = jobs.Count(),
                        QualityOfWorks = feedBacks.Select(p=>p.QualityOfWork),                    

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
