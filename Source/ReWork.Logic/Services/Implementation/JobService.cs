using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReWork.Logic.Services.Implementation
{
    public class JobService : IJobService
    {
        private IJobRepository _jobRepository;
        private ICustomerProfileRepository _customerProfileRepository;
        private ISkillRepository _skillRepository;
        private ICommitProvider _commitProvider;

        public JobService(IJobRepository jobRep, ICustomerProfileRepository customerProfileRep, ISkillRepository skillRep, ICommitProvider commitProvider)
        {
            _jobRepository = jobRep;
            _customerProfileRepository = customerProfileRep;
            _skillRepository = skillRep;
            _commitProvider = commitProvider;
        }

        public void CreateJob(CreateJobParams jobParams)
        {
            CustomerProfile customerProfile = _customerProfileRepository.FindCustomerProfileByName(jobParams.CustomerUserName);
            if(customerProfile != null)
            {
                Job job = new Job()
                { Customer = customerProfile, Title = jobParams.Title, Description = jobParams.Description, Price = jobParams.Price, PriceDiscussed = jobParams.PriceDiscussed, DateAdded = DateTime.Now };

                foreach (var id in jobParams.SkillsId)
                {
                    Skill skill = _skillRepository.FindById(id);
                    job.Skills.Add(skill);
                }

                _jobRepository.Create(job);
                _commitProvider.SaveChanges();
            } 
        }

        public void DeleteJob(int id)
        {
            Job job = _jobRepository.FindJobById(id);
            if(job != null)
            {
                _jobRepository.Delete(job);
                _commitProvider.SaveChanges();
            }
        }
    }
}
