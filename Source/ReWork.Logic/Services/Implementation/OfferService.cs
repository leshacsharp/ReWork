using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
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

        public void CreateOffer(CreateOfferParams offerParams)
        {
            EmployeeProfile employee = _employeeRepository.FindEmployeeProfileById(offerParams.EmployeeId);
            Job job = _jobRepository.FindJobById(offerParams.JobId);

            if (employee != null && job != null) 
            {
                bool existsOfferOnThisJob = employee.Offers.Any(p => p.JobId.Equals(offerParams.JobId));
                if (!existsOfferOnThisJob)
                {
                    Offer offer = new Offer()
                    { Job = job, Employee = employee, Text = offerParams.Text, AddedDate = DateTime.UtcNow, OfferPayment = offerParams.OfferPayment, ImplementationDays = offerParams.ImplementationDays };

                    _offerRepository.Create(offer);
                }
            }
        }

        public IEnumerable<OfferInfo>FindJobOffers(int jobId)
        {
            Job job = _jobRepository.FindJobById(jobId);
            if(job != null)
            {
                return _offerRepository.FindJobOffers(jobId);
            }

            return null;
        }
    }
}
