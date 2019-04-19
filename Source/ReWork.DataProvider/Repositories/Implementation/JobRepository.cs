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
            return Db.Jobs.Where(predicate).Select(
                     j => new JobInfo()
                     {
                         Id = j.Id,
                         Title = j.Title,
                         Description = j.Description,
                         Price = j.Price,
                         PriceDiscussed = j.PriceDiscussed,
                         DateAdded = (DateTime)j.DateAdded,
                         CountOffers = j.Offers.Count(),

                         SkillsId = j.Skills.Select(p => p.Id),
                         Skills = j.Skills.Select(p => p.Title)
                     });
        }


        public JobInfo FindJobInfoById(int id)
        {
            return Db.Jobs.Where(p => p.Id == id).Select(j =>
                      new JobInfo()
                      {
                          Id = j.Id,
                          Title = j.Title,
                          Description = j.Description,
                          Price = j.Price,
                          PriceDiscussed = j.PriceDiscussed,
                          DateAdded = (DateTime)j.DateAdded,
                          CountOffers = j.Offers.Count(),

                          SkillsId = j.Skills.Select(p => p.Id),
                          Skills = j.Skills.Select(p => p.Title)
                      }).SingleOrDefault();
        }



        public void Update(Job item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
