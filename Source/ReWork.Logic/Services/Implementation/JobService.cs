using AutoMapper;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.UnitOfWork;
using ReWork.Logic.Dto;
using ReWork.Logic.Services.Abstraction;
using System;
using System.Linq;
using ReWork.Common;

namespace ReWork.Logic.Services.Implementation
{
    public class JobService : IJobService
    {
        private IUnitOfWork _db;
        public JobService(IUnitOfWork db)
        {
            _db = db;
        }

        public void CreateJob(JobDto job)
        {
            CustomerProfile customerProfile = _db.CustomerProfileRepository.FindCustomerProfileByName(job.Customer.UserName);
            if(customerProfile != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobDto, Job>()).CreateMapper();
                Job newJob = mapper.Map<JobDto, Job>(job);

                newJob.Customer = customerProfile;
                foreach (var skill in job.Skills)
                {
                    Skill addedSkill = _db.SkillRepository.FindSkillByTitle(skill.Title);
                    newJob.Skills.Add(addedSkill);
                }

                _db.JobRepository.Create(newJob);
                _db.SaveChanges();
            }
        }

        public JobDto GetJobById(int id)
        {
            Job job = _db.JobRepository.GetJobById(id);
            return Mapping<Job, JobDto>.MapObject(job);
        }
    }
}
