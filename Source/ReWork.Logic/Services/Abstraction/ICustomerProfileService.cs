namespace ReWork.Logic.Services.Abstraction
{
    public interface ICustomerProfileService
    {
        void CreateCustomerProfile(string userName);
        bool CustomerProfileExists(string userName);
    }
}
