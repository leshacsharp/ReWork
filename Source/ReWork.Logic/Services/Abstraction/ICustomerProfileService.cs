using ReWork.Model.Entities;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ICustomerProfileService
    {
        void CreateCustomerProfile(string userName);
        void DeleteCustomerProfile(string customerId);

        bool CustomerProfileExists(string userName);
    }
}
