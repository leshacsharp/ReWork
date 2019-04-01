using ReWork.Logic.Dto;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IEmployeeProfileService
    {
        void CreateEmployeeProfile(EmployeeProfileDto employee);
        void EditEmployeeProfile(EmployeeProfileDto employee);
    }
}
