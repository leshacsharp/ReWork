using FirstQuad.Common.Helpers;
using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.Entities.Common;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;

namespace ReWork.Logic.Services.Implementation
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        private UserManager<User> _userManager;
        private ICustomerProfileRepository _customerRepository;
        private ISkillRepository _skillRepository;

        public JobService(IJobRepository jobRep, UserManager<User> userManager, ICustomerProfileRepository customerRep, IEmployeeProfileRepository employeeRep, ISkillRepository skillRep)
        {
            _jobRepository = jobRep;
            _userManager = userManager;
            _customerRepository = customerRep;
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

        public void FinishJob(int jobId)
        {
            var job = _jobRepository.FindJobById(jobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={jobId} not found");

            job.Status = ProjectStatus.Finish;
            _jobRepository.Update(job);
        }

        public void ViewJob(int jobId, string userId)
        {
            var job = _jobRepository.FindJobById(jobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={jobId} not found");

            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            bool userViewExists = user.ViewedJobs.Any(p => p.Id == jobId);
            if (userViewExists)
                throw new ArgumentException($"User with id={userId} already see job with id={jobId}");

            job.ViewedUsers.Add(user);

            _jobRepository.Update(job);
        }

        public bool UserViewExists(int jobId, string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            return user.ViewedJobs.Any(p => p.Id == jobId);
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
            filter = filter.AndAlso<Job>(j => j.CustomerId == customerId && j.Status != ProjectStatus.Finish);

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
            filter = filter.AndAlso<Job>(j => j.EmployeeId == employeeId && j.Status != ProjectStatus.Finish);

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
            Expression<Func<Job, bool>> filter = JobFilter(skillsId, keyWords, priceFrom);

            return _jobRepository.FindJobsInfo(filter)
                                 .OrderByDescending(p => p.DateAdded)
                                 .ToList();
        }

        public IEnumerable<JobInfo> FindRelevantJobs(int[] skillsId, string keyWords, int priceFrom, int[] emloyeeSkillsId = null)
        {
            Expression<Func<Job, bool>> filter = JobFilter(skillsId, keyWords, priceFrom);

            const int skillsPercent = 40;
            const int pricePercent = 30;
            const int datePercent = 20;
            const int viewsPercent = 10;

            const double maxPrice = 100000;
            const double maxViews = 100000;
            const double maxHours = 4320; //180 days

            var jobSkills = skillsId == null ? Enumerable.Empty<int>() : skillsId;
            var employeeSkills = emloyeeSkillsId == null ? Enumerable.Empty<int>() : emloyeeSkillsId;
            var skillsForSort = jobSkills.Union(employeeSkills);
            double maxSkillsCountForSort = skillsForSort.Count() == 0 ? 1 : skillsForSort.Count();

            Expression<Func<JobInfo, double>> sortByRelevant = (j) =>
                      (j.Skills.Count(s => skillsForSort.Contains(s.Id)) / maxSkillsCountForSort * skillsPercent) +
                      (j.Price / maxPrice * pricePercent) +
                      (j.CountViews / maxViews * viewsPercent) -
                      ((double)DbFunctions.DiffHours(j.DateAdded, DateTime.UtcNow) / maxHours * datePercent);


            return _jobRepository.FindJobsInfo(filter)
                                 .OrderByDescending(sortByRelevant)
                                 .ToList();
        }


        private Expression<Func<Job, bool>> JobFilter(int[] skillsId, string keyWords, int priceFrom)
        {
            var filter = PredicateBuilder.True<Job>();
            filter = filter.AndAlso(job => job.Status == ProjectStatus.Open);

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

            return filter;
        }
    }
}
