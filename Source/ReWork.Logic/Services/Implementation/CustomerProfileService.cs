using System;
using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using System.Data.Entity.Core;

namespace ReWork.Logic.Services.Implementation
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private ICustomerProfileRepository _customerRepository;
        private UserManager<User> _userManager;

        public CustomerProfileService(ICustomerProfileRepository customerRep, UserManager<User> userManager)
        {
            _customerRepository = customerRep;   
            _userManager = userManager;
        }

        public void CreateCustomerProfile(string userId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            if (user.CustomerProfile != null)
                throw new ArgumentException($"Customer profile with id={userId} already exists", "CustomerProfile");


            var customerProfile = new CustomerProfile() { User = user };
            _customerRepository.Create(customerProfile);
        }

        public void DeleteCustomerProfile(string customerId)
        {
            var customer = _customerRepository.FindCustomerProfileById(customerId);
            if (customer == null)
                throw new ObjectNotFoundException($"Customer profile with id={customerId} not found");
            
            _customerRepository.Delete(customer);  
        }


        public bool CustomerProfileExists(string userId)
        {
            CustomerProfile customerProfile = _customerRepository.FindCustomerProfileById(userId);
            return customerProfile != null;
        }
    }
}
