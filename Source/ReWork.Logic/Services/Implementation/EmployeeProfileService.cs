using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using System.Collections.Generic;

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


        public void CreateEmployeeProfile(string userName, int age, IEnumerable<int> skillsId)
        {
            User user = _userManager.FindByName(userName);
            if (user != null && user.EmployeeProfile == null)
            {
                EmployeeProfile employeeProfile = new EmployeeProfile() { User = user, Age = age };

                foreach (var id in skillsId)
                {
                    Skill skill = _skillRepository.FindById(id);
                    employeeProfile.Skills.Add(skill);
                }

                _employeeRepository.Create(employeeProfile);
                _commitProvider.SaveChanges();
            }  
        }

        public void EditEmployeeProfile(string employeeId, int age, IEnumerable<int> skillsId)
        {
            //TODO: переделать

            //EmployeeProfile employeeProfile = _employeeRepository.FindEmployeeProfileById(employeeId);
            //if (employeeProfile != null)
            //{
            //    List<Skill> newSkills = new List<Skill>();
            //    foreach (var id in skillsId)
            //    {
            //        Skill skill = _skillRepository.FindById(id);
            //        newSkills.Add(skill);
            //    }

            //    employeeProfile.Age = age;
            //    employeeProfile.Skills = newSkills;

            //    _employeeRepository.Update(employeeProfile);
            //    _commitProvider.SaveChanges();
            //}
        }

        public void DeleteEmployeeProfile(string employeeId)
        {
            EmployeeProfile employee = _employeeRepository.FindEmployeeProfileById(employeeId);
            if(employee != null)
            {
                _employeeRepository.Delete(employee);
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
            return _employeeRepository.FindEmployeeProfileById(id);
        }
    }
}
