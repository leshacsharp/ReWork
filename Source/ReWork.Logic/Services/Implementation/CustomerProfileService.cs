using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.UnitOfWork;
using ReWork.Logic.Services.Abstraction;
using System;

namespace ReWork.Logic.Services.Implementation
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private IUnitOfWork _db;
        public CustomerProfileService(IUnitOfWork db)
        {
            _db = db;
        }

        public void CreateCustomerProfile(string userName)
        {
            User user = _db.UserManager.FindByName(userName);
            if(user != null && user.CustomerProfile == null)
            {
                CustomerProfile customerProfile = new CustomerProfile() { User = user };
                _db.CustomerProfileRepository.Create(customerProfile);

                _db.SaveChanges();
            }
        }
    }
}
