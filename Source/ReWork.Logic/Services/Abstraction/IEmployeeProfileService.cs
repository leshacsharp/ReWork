using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;

namespace ReWork.Logic.Services.Abstraction
{
    public interface IEmployeeProfileService
    {
        void CreateEmployeeProfile(string userId, int age, string aboutMe, int[] skillsId);
        void EditEmployeeProfile(string employeeId, int age, string aboutMe, int[] skillsId);
        void DeleteEmployeeProfile(string employeeId);

        IEnumerable<EmployeeProfileInfo> FindEmployes(int [] skillsId, string keyWords);
        EmployeeProfileInfo FindEmployeeInfoById(string userId);  
        bool EmployeeProfileExists(string userId);
    }
}
