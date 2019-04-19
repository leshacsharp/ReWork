using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
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

        public IEnumerable<OfferInfo> FindJobOffers(int jobId)
        {
            return  from o in Db.Offers
                    join e in Db.EmployeeProfiles on o.EpmployeeId equals e.Id
                    join u in Db.Users on e.Id equals u.Id 
                    where o.JobId == jobId
                    select new OfferInfo
                    {
                        Id = o.Id,
                        Text = o.Text,
                        AddedDate = o.AddedDate,
                        ImplementationDays = o.ImplementationDays,
                        OfferPayment = o.OfferPayment,

                        EmployeeId = e.Id,
                        UserName = u.UserName
                    };
        }

        public IEnumerable<Offer> FindOffersByUserId(string userId)
        {
            return Db.Offers.Where(p => p.Employee.Id == userId).ToList();
        }

        public void Update(Offer item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
