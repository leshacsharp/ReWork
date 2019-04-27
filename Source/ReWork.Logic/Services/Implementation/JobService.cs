﻿using FirstQuad.Common.Helpers;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ReWork.Model.Entities.Common;

namespace ReWork.Logic.Services.Implementation
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        private ICustomerProfileRepository _customerProfileRepository;
        private ISkillRepository _skillRepository;

        public JobService(IJobRepository jobRep, ICustomerProfileRepository customerProfileRep, ISkillRepository skillRep)
        {
            _jobRepository = jobRep;
            _customerProfileRepository = customerProfileRep;
            _skillRepository = skillRep;
        }

        public void CreateJob(CreateJobParams jobParams)
        {
            CustomerProfile customerProfile = _customerProfileRepository.FindCustomerProfileByName(jobParams.CustomerUserName);
            if (customerProfile != null)
            {
                Job job = new Job()
                { Customer = customerProfile, Title = jobParams.Title, Description = jobParams.Description, Price = jobParams.Price, PriceDiscussed = jobParams.PriceDiscussed, DateAdded = DateTime.Now };

                _jobRepository.Create(job);

                foreach (var skillId in jobParams.SkillsId)
                {
                    Skill skill = _skillRepository.FindById(skillId);
                    job.Skills.Add(skill);
                }

                _jobRepository.Create(job);
            }
        }

        public void Edit(EditJobParams editJobParams)
        {
            Job job = _jobRepository.FindJobById(editJobParams.Id);
            if (job != null)
            {
                job.Title = editJobParams.Title;
                job.Description = editJobParams.Description;
                job.Price = editJobParams.Price;
                job.PriceDiscussed = editJobParams.PriceDiscussed;

                job.Skills.Clear();
                foreach (var skillId in editJobParams.SkillsId)
                {
                    Skill skill = _skillRepository.FindById(skillId);
                    job.Skills.Add(skill);
                }

                _jobRepository.Update(job);
            }
        }

        public void DeleteJob(int jobId)
        {
            Job job = _jobRepository.FindJobById(jobId);
            if(job != null)
            {
                _jobRepository.Delete(job);
            }
        }
 


        public JobInfo FindById(int jobId)
        {
             return _jobRepository.FindJobInfoById(jobId);
        }

        public IEnumerable<JobInfo> FindUserJobs(string customerId, DateTime? fromDate)
        {
            var filter = PredicateBuilder.True<Job>();
            filter = filter.AndAlso<Job>(job=>job.CustomerId == customerId);

            if(fromDate != null)
            {
                filter = filter.AndAlso<Job>(job => job.DateAdded >= fromDate);
            }

            return _jobRepository.FindJobsInfo(filter)
                                 .OrderByDescending(p => p.DateAdded)
                                 .ToList();
        }

        public IEnumerable<JobInfo> FindJobs(int[] skillsId, string keyWords, int priceFrom)
        {
            var filter = PredicateBuilder.True<Job>();
            filter = filter.AndAlso<Job>(job => job.Status == ProjectStatus.Open);

            if (skillsId != null && skillsId.Length > 0)
            {
                filter = filter.AndAlso<Job>(job => job.Skills.Any(p => skillsId.Contains(p.Id)));
            }

            if (!String.IsNullOrWhiteSpace(keyWords))
            {
                filter = filter.AndAlso(p => p.Title.Contains(keyWords) || p.Description.Contains(keyWords));
            }

            if (priceFrom > 0)
            {
                filter = filter.AndAlso(p => p.Price >= priceFrom);
            }

            return _jobRepository.FindJobsInfo(filter)
                                 .OrderByDescending(p => p.DateAdded)
                                 .ToList();
        }
    }
}