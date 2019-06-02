using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Data.Entity;
using System.Linq;

namespace ReWork.DataProvider.Repositories.Implementation
{
    public class CustomerProfileRepository : BaseRepository, ICustomerProfileRepository
    {
        public void Create(CustomerProfile item)
        {
            Db.CustomerProfiles.Add(item);
        }

        public void Delete(CustomerProfile item)
        {
            Db.CustomerProfiles.Remove(item);
        }

        public CustomerProfile FindCustomerProfileById(string customerId)
        {
            return Db.CustomerProfiles.Find(customerId);
        }

        public CustomerProfileInfo FindCustomerProfileInfo(string customerId)
        {
            return (from c in Db.CustomerProfiles
                    join u in Db.Users on c.Id equals u.Id
                    where c.Id == customerId
                    select new CustomerProfileInfo()
                    {
                        Id = c.Id,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        RegistrationdDate = u.RegistrationdDate,
                        Image = u.Image,
                        CountPublishJobs = c.Jobs.Count,
                        QualityOfWorks = u.RecivedFeedBacks.Select(p => p.QualityOfWork)
                    }).SingleOrDefault();
        }

        public void Update(CustomerProfile item)
        {
            Db.Entry(item).State = EntityState.Modified;
        }
    }
}
