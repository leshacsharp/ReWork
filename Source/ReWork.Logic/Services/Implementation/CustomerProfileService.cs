using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;

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
            User user = _userManager.FindById(userId);
            if(user != null && user.CustomerProfile == null)
            {
                CustomerProfile customerProfile = new CustomerProfile() { User = user };
                _customerRepository.Create(customerProfile); 
            }
        }

        public void DeleteCustomerProfile(string customerId)
        {
            CustomerProfile customer = _customerRepository.FindCustomerProfileById(customerId);
            if(customer != null)
            {
                _customerRepository.Delete(customer);
            }
        }


        public bool CustomerProfileExists(string userName)
        {
            User user = _userManager.FindByName(userName);
            if(user != null)
            {
                return user.CustomerProfile != null;
            }

            return false;
        }
    
    }
}
