using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Entities.Common;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class OfferRepository : BaseRepository, IOfferRepository
    {
        public void Create(Offer item)
        {
            Db.Offers.Add(item);
        }

        public void Delete(Offer item)
        {
            Db.Offers.Remove(item);
        }

        public Offer FindOfferById(int offerId)
        {
            return Db.Offers.Find(offerId);
        }

        public Offer FindOffer(int jobId, string employeeId)
        {
            return Db.Offers.SingleOrDefault(o => o.JobId == jobId && o.EpmployeeId == employeeId);
        }

        public IEnumerable<OfferInfo> FindJobOffers(int jobId)
        {
            return (from o in Db.Offers
                    join e in Db.EmployeeProfiles on o.EpmployeeId equals e.Id
                    join u in Db.Users on e.Id equals u.Id
                    where o.JobId == jobId
                    select new OfferInfo
                    {
                        Text = o.Text,
                        AddedDate = o.AddedDate,
                        ImplementationDays = o.ImplementationDays,
                        OfferPayment = o.OfferPayment,

                        EmployeeImage = u.Image,
                        EmployeeId = e.Id,
                        UserName = u.UserName
                    }).ToList();
        }

        public IEnumerable<EmployeeOfferInfo> FindEmployeeOffers(string employeeId)
        {
            return from o in Db.Offers
                   join j in Db.Jobs on o.JobId equals j.Id
                   where o.EpmployeeId == employeeId
                   select new EmployeeOfferInfo
                   {
                       Text = o.Text,
                       AddedDate = o.AddedDate,
                       ImplementationDays = o.ImplementationDays,
                       OfferPayment = o.OfferPayment,
                       
                       JobId = j.Id,
                       JobTitle = j.Title,
                       JobPrice = j.Price,
                       JobAdded = j.DateAdded
                   };
        }

        public IEnumerable<CustomerOfferInfo> FindCustomerOffers(string customerId)
        {
            return (from o in Db.Offers
                    join j in Db.Jobs on o.JobId equals j.Id
                    join e in Db.EmployeeProfiles on o.EpmployeeId equals e.Id
                    join u in Db.Users on e.Id equals u.Id
                    where j.CustomerId == customerId && o.OfferStatus == OfferStatus.Waited && j.EmployeeId == null
                    select new CustomerOfferInfo()
                    {
                        Id = o.Id,
                        Text = o.Text,
                        AddedDate = o.AddedDate,
                        ImplementationDays = o.ImplementationDays,
                        OfferPayment = o.OfferPayment,

                        EmployeeImage = u.Image,
                        EmployeeId = e.Id,
                        UserName = u.UserName,
                        UserDateRegistration = u.RegistrationdDate,

                        JobTitle = j.Title,
                        JobId = j.Id
                    }).ToList();
        }

        public void Update(Offer item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
