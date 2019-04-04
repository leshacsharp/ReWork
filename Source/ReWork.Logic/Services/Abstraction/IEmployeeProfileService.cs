using ReWork.Model.Entities;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IEmployeeProfileService
    {
        void CreateEmployeeProfile(EmployeeProfile employee);
        EmployeeProfile GetEmployeeProfileById(string id);
    }
}
