using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;

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

        public void CreateEmployeeProfile(EmployeeProfile employee)
        {
            User user = _userManager.FindByName(employee.User.UserName);
            if (user != null && user.EmployeeProfile == null)
            {
                EmployeeProfile employeeProfile = new EmployeeProfile() { User = user, Age = employee.Age };            
                foreach (var it in employee.Skills)
                {
                    Skill skill = _skillRepository.FindSkillByTitle(it.Title);
                    employeeProfile.Skills.Add(skill);
                }

                _employeeRepository.Create(employeeProfile);
                _commitProvider.SaveChanges();
            }  
        }

        public EmployeeProfile GetEmployeeProfileById(string id)
        {
            return _employeeRepository.GetEmployeeProfileById(id);
        }
    }
}
