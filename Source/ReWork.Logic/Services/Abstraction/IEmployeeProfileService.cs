using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IEmployeeProfileService
    {
        void CreateEmployeeProfile(EmployeeProfile employeeProfile);
        void EditEmpployeeProfile(EmployeeProfile employeeProfile);
        EmployeeProfile GetEmployeeProfileById(string id);  
        bool EmployeeProfileExists(string userName);
    }
}
