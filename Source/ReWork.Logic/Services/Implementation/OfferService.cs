using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.Entities.Common;
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

        public void AcceptOffer(int offerId, string employeeId)
        {
            var offer = _offerRepository.FindOfferById(offerId);
            if (offer == null)
                throw new ObjectNotFoundException($"Offer with id={offerId} not found");

            var employee = _employeeRepository.FindEmployeeById(employeeId);
            if (employee == null)
                throw new ObjectNotFoundException($"Employee profile with id={employeeId} not found");


            offer.OfferStatus = OfferStatus.Accepted;
            offer.Job.Employee = employee;
            offer.Job.Status = ProjectStatus.Closed;

            _offerRepository.Update(offer);
        }

        public void RejectOffer(int offerId)
        {
            var offer = _offerRepository.FindOfferById(offerId);
            if (offer == null)
                throw new ObjectNotFoundException($"Offer with id={offerId} not found");

            offer.OfferStatus = OfferStatus.Rejected;

            _offerRepository.Update(offer);
        }

        public void CreateOffer(CreateOfferParams offerParams)
        {
            var employee = _employeeRepository.FindEmployeeById(offerParams.EmployeeId);
            if (employee == null)
                throw new ObjectNotFoundException($"Employee profile with id={offerParams.EmployeeId} not found");

            var job = _jobRepository.FindJobById(offerParams.JobId);
            if (job == null)
                throw new ObjectNotFoundException($"Job with id={offerParams.JobId} not found");


            var offerForJob = _offerRepository.FindOffer(offerParams.JobId, offerParams.EmployeeId);
            if (offerForJob != null)
                throw new ArgumentException($"At user with id={employee.Id} already have offer to job with id={job.Id}");


            var offer = new Offer()
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

        public bool OfferExists(int jobId, string employeeId)
        {
            var offer = _offerRepository.FindOffer(jobId, employeeId);
            return offer != null;
        }
    }
}
