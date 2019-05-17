namespace ReWork.Logic.Services.Abstraction
{
    public interface ICustomerProfileService
    {
        void CreateCustomerProfile(string userId);
        void DeleteCustomerProfile(string customerId);

        bool CustomerProfileExists(string userId);
    }
}
