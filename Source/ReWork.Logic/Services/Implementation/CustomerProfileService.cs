using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.UnitOfWork;
using ReWork.Logic.Dto;
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
            if (userName == null)
                throw new ArgumentNullException("userName", "userName not can be null");

            User user = _db.UserManager.FindByName(userName);
            CustomerProfile customerProfile = new CustomerProfile() { User = user };
            _db.CustomerProfileRepository.Create(customerProfile);

            _db.SaveChanges();
        }

        public void EditCustomerProfile(CustomerProfileDto customer)
        {
            throw new NotImplementedException();
        }
    }
}
