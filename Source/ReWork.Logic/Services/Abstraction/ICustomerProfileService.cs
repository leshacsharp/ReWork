using ReWork.Logic.Dto;

namespace ReWork.Logic.Services.Abstraction
{
    public interface ICustomerProfileService
    {
        void CreateCustomerProfile(string userName);
        void EditCustomerProfile(CustomerProfileDto customer);
    }
}
