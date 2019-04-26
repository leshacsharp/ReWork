using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IEmployeeProfileService
    {
        void CreateEmployeeProfile(string userName, int age, int[] skillsId);
        void EditEmployeeProfile(string employeeId, int age, int[] skillsId);
        void DeleteEmployeeProfile(string employeeId);

        IEnumerable<EmployeeProfileInfo> FindEmployes(int [] skillsId);
        EmployeeProfileInfo FindEmployeeInfoById(string id);  
        bool EmployeeProfileExists(string userName);
    }
}
