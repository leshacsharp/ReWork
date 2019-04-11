using ReWork.Model.Entities;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IEmployeeProfileService
    {
        void CreateEmployeeProfile(string userName, int age, IEnumerable<int> skillsId);
        void EditEmployeeProfile(string employeeId, int age, IEnumerable<int> skillsId);
        void DeleteEmployeeProfile(string employeeId);

        EmployeeProfile GetEmployeeProfileById(string id);  
        bool EmployeeProfileExists(string userName);
    }
}
