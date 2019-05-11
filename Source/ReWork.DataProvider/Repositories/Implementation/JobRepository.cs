using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class JobRepository : BaseRepository, IJobRepository
    { 
        public void Create(Job item)
        {
            Db.Jobs.Add(item);
        }

        public void Delete(Job item)
        {
            Db.Jobs.Remove(item);
        }

        public Job FindJobById(int id)
        {
            return Db.Jobs.Find(id);
        }

        public IQueryable<JobInfo> FindJobsInfo(Expression<Func<Job, bool>> predicate)
        {
            return from j in Db.Jobs.Where(predicate)
                   join c in Db.CustomerProfiles on j.CustomerId equals c.Id
                   join u in Db.Users on c.Id equals u.Id
                   select new JobInfo()
                   {
                       Id = j.Id,
                       Title = j.Title,
                       Description = j.Description,
                       Price = j.Price,
                       PriceDiscussed = j.PriceDiscussed,
                       DateAdded = (DateTime)j.DateAdded,
                       CountOffers = j.Offers.Count,

                       UserName = u.UserName,
                       CustomerId = c.Id,

                       Skills = j.Skills.Select(p => new SkillInfo()
                       {
                           Id = p.Id,
                           Title = p.Title
                       })
                   };
        }
      

        public MyJobInfo FindMyJobInfo(int id)
        {
            return (from j in Db.Jobs
                    join e in Db.EmployeeProfiles on j.EmployeeId equals e.Id into eJoin
                    from e in eJoin.DefaultIfEmpty()
                    join u in Db.Users on e.Id equals u.Id into uJoin
                    from u in uJoin.DefaultIfEmpty()
                    where j.Id == id
                    select new MyJobInfo()
                    {
                        Title = j.Title,
                        Price = j.Price,
                        PriceDiscussed = j.PriceDiscussed,
                        DateAdded = (DateTime)j.DateAdded,
                       
                        EmployeeId = (e != null ? e.Id : null),
                        EmployeeUserName = (u != null ? u.UserName : null),

                        Skills = j.Skills.Select(p => new SkillInfo()
                        {
                            Id = p.Id,
                            Title = p.Title
                        })
                    }).SingleOrDefault();
        }



        public void Update(Job item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
