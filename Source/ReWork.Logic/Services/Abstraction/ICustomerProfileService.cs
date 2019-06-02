using ReWork.Model.EntitiesInfo;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ICustomerProfileService
    {
        void CreateCustomerProfile(string userId);
        void DeleteCustomerProfile(string customerId);

        CustomerProfileInfo FindCustomerProfile(string customerId);

        bool CustomerProfileExists(string userId);
    }
}
