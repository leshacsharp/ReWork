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
            return Db.Jobs.Where(predicate).Select(j => new JobInfo()
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                Price = j.Price,
                PriceDiscussed = j.PriceDiscussed,
                DateAdded = (DateTime)j.DateAdded,
                CountOffers = j.Offers.Count(),

                UserName = j.Customer.User.UserName,
                CustomerId = j.CustomerId,

                Skills = j.Skills.Select(p => new SkillInfo()
                {
                    Id = p.Id,
                    Title = p.Title
                })
            });
        }


        public JobInfo FindJobInfoById(int id)
        {
            return (from j in Db.Jobs
                    join c in Db.CustomerProfiles on j.CustomerId equals c.Id
                    join u in Db.Users on c.Id equals u.Id
                    where j.Id == id
                    select new JobInfo()
                    {
                        Id = j.Id,
                        Title = j.Title,
                        Description = j.Description,
                        Price = j.Price,
                        PriceDiscussed = j.PriceDiscussed,
                        DateAdded = (DateTime)j.DateAdded,
                        CountOffers = j.Offers.Count(),

                        UserName = u.UserName,
                        CustomerId = c.Id,

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
