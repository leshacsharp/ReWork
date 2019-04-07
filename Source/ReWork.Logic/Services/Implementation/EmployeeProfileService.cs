using Microsoft.AspNet.Identity;
using ReWork.Common;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Logic.Services.Params;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels;

namespace ReWork.Logic.Services.Implementation
{
    public class EmployeeProfileService : IEmployeeProfileService
    {
        private IEmployeeProfileRepository _employeeRepository;
        private ISkillRepository _skillRepository;
        private ICommitProvider _commitProvider;
        private UserManager<User> _userManager;

        public EmployeeProfileService(IEmployeeProfileRepository employeeRep, ISkillRepository skillRep, ICommitProvider commitProvider, UserManager<User> userManager)
        {
            _employeeRepository = employeeRep;
            _skillRepository = skillRep;
            _commitProvider = commitProvider;
            _userManager = userManager;
        }

        public void CreateEmployeeProfile(EmployeeProfile employeeProfile)
        {
            User user = _userManager.FindByName(employeeProfile.User.UserName);
            if (user != null && user.EmployeeProfile == null)
            {
                EmployeeProfile newEmployeeProfile = new EmployeeProfile() { User = user, Age = employeeProfile.Age };

                foreach (var it in employeeProfile.Skills)
                {
                    Skill skill = _skillRepository.GetById(it.Id);
                    newEmployeeProfile.Skills.Add(skill);
                }     

                _employeeRepository.Create(newEmployeeProfile);
                _commitProvider.SaveChanges();
            }  
        }

        public void EditEmpployeeProfile(EmployeeProfile employeeProfile)
        {
            EmployeeProfile employee = _employeeRepository.GetEmployeeProfileById(employeeProfile.Id);
            if(employee != null)
            {
                _employeeRepository.Update(employeeProfile);
                _commitProvider.SaveChanges();
            }
        }

        public bool EmployeeProfileExists(string userName)
        {
            User user = _userManager.FindByName(userName);
            if (user != null)
            {
                return user.EmployeeProfile != null;
            }

            return false;
        }

        public EmployeeProfile GetEmployeeProfileById(string id)
        {
            return _employeeRepository.GetEmployeeProfileById(id);
        }
    }
}
