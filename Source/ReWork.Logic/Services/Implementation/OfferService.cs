using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;

namespace ReWork.Logic.Services.Implementation
{
    public class OfferService : IOfferService
    {
        private IOfferRepository _offerRepository;
        private IEmployeeProfileRepository _employeeRepository;
        private IJobRepository _jobRepository;

        public OfferService(IOfferRepository offerRep, IEmployeeProfileRepository employeeRep, IJobRepository jobRep)
        {
            _offerRepository = offerRep;
            _employeeRepository = employeeRep;
            _jobRepository = jobRep;
        }

        public void AcceptOffer(int jobId, string employeeId)
        {
            Job job = _jobRepository.FindJobById(jobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={jobId} not found");

            EmployeeProfile employee = _employeeRepository.FindEmployeeById(employeeId);
            if (employee == null)
                throw new ObjectNotFoundException($"Employee profile with id={employeeId} not found");

            job.Employee = employee;
            job.Status = Model.Entities.Common.ProjectStatus.Closed;

            _jobRepository.Update(job);
        }


        public void CreateOffer(CreateOfferParams offerParams)
        {
            EmployeeProfile employee = _employeeRepository.FindEmployeeById(offerParams.EmployeeId);
            if (employee == null)
                throw new ObjectNotFoundException($"Employee profile with id={offerParams.EmployeeId} not found");

            Job job = _jobRepository.FindJobById(offerParams.JobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={offerParams.JobId} not found");


            bool existsOfferOnThisJob = employee.Offers.Any(p => p.JobId == offerParams.JobId);
            if (existsOfferOnThisJob)
                throw new ArgumentException($"At user with id={employee.Id} already have offer to job with id={job.Id}");


            Offer offer = new Offer()
            {
                Job = job,
                Employee = employee,
                Text = offerParams.Text,
                AddedDate = DateTime.UtcNow,
                OfferPayment = offerParams.OfferPayment,
                ImplementationDays = offerParams.ImplementationDays
            };

            _offerRepository.Create(offer);
        }


        public IEnumerable<CustomerOfferInfo> FindCustomerOffers(string customerId)
        {
            return _offerRepository.FindCustomerOffers(customerId);
        }


        public IEnumerable<EmployeeOfferInfo> FindEmployeeOffers(string employeeId)
        {
            return _offerRepository.FindEmployeeOffers(employeeId); 
        }


        public IEnumerable<OfferInfo>FindJobOffers(int jobId)
        {
            return _offerRepository.FindJobOffers(jobId);
        }
    }
}
