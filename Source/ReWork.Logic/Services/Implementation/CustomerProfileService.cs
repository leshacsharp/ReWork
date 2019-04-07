using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;

namespace ReWork.Logic.Services.Implementation
{
    public class CustomerProfileService : ICustomerProfileService
    {
        private ICustomerProfileRepository _customerRepository;
        private ICommitProvider _commitProvider;
        private UserManager<User> _userManager;

        public CustomerProfileService(ICustomerProfileRepository customerRep, ICommitProvider commitProvider, UserManager<User> userManager)
        {
            _customerRepository = customerRep;
            _commitProvider = commitProvider;
            _userManager = userManager;
        }

        public void CreateCustomerProfile(string userName)
        {
            User user = _userManager.FindByName(userName);
            if(user != null && user.CustomerProfile == null)
            {
                CustomerProfile customerProfile = new CustomerProfile() { User = user };
                _customerRepository.Create(customerProfile);

                _commitProvider.SaveChanges();
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
