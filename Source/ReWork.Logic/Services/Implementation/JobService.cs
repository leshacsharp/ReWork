using FirstQuad.Common.Helpers;
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
using System.Data.Entity.Core;

namespace ReWork.Logic.Services.Implementation
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        private ICustomerProfileRepository _customerRepository;
        private IEmployeeProfileRepository _employeeRepository;
        private ISkillRepository _skillRepository;

        public JobService(IJobRepository jobRep, ICustomerProfileRepository customerRep, IEmployeeProfileRepository employeeRep, ISkillRepository skillRep)
        {
            _jobRepository = jobRep;
            _customerRepository = customerRep;
            _employeeRepository = employeeRep;
            _skillRepository = skillRep;
        }

        public void CreateJob(CreateJobParams jobParams)
        {
            var customerProfile = _customerRepository.FindCustomerProfileById(jobParams.CustomerId);
            if(customerProfile == null)
                throw new ObjectNotFoundException($"Customer profile with id={jobParams.CustomerId} not found");
 
            var job = new Job()
            {
                Customer = customerProfile,
                Title = jobParams.Title,
                Description = jobParams.Description,
                Price = jobParams.Price,
                PriceDiscussed = jobParams.PriceDiscussed,
                DateAdded = DateTime.UtcNow
            };


            foreach (var skillId in jobParams.SkillsId)
            {
                Skill skill = _skillRepository.FindById(skillId);

                if (skill == null)
                    throw new ObjectNotFoundException($"Skill with id={skillId} not found");

                job.Skills.Add(skill);
            }

            _jobRepository.Create(job);
        }

        public void Edit(EditJobParams editJobParams)
        {
            var job = _jobRepository.FindJobById(editJobParams.JobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={editJobParams.JobId} not found");

            job.Title = editJobParams.Title;
            job.Description = editJobParams.Description;
            job.Price = editJobParams.Price;
            job.PriceDiscussed = editJobParams.PriceDiscussed;

            job.Skills.Clear();
            foreach (var skillId in editJobParams.SkillsId)
            {
                Skill skill = _skillRepository.FindById(skillId);

                if (skill == null)
                    throw new ObjectNotFoundException($"Skill with id={skillId} not found");

                job.Skills.Add(skill);
            }

            _jobRepository.Update(job);
        }

        public void DeleteJob(int jobId)
        {
            var job = _jobRepository.FindJobById(jobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={jobId} not found");

            _jobRepository.Delete(job);
        }

        public void DeleteEmployeeFromJob(int jobId)
        {
            var job = _jobRepository.FindJobById(jobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={jobId} not found");   

            if(job.Employee == null)
                throw new ObjectNotFoundException($"Employee profile in job with id={jobId} not found");

            job.Status = ProjectStatus.Open;
            job.EmployeeId = null;
        }



        public JobInfo FindJob(int jobId)
        {
            return _jobRepository.FindJobsInfo(p => p.Id == jobId).SingleOrDefault();
        }

        public MyJobInfo FindCustomerJob(int jobId)
        {
            return _jobRepository.FindMyJobInfo(jobId);
        }


        public IEnumerable<JobInfo> FindCustomerJobs(string customerId, DateTime? fromDate)
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

        public IEnumerable<JobInfo> FindEmployeeJobs(string employeeId, DateTime? fromDate)
        {
            var filter = PredicateBuilder.True<Job>();
            filter = filter.AndAlso<Job>(job => job.EmployeeId == employeeId);

            if (fromDate != null)
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
