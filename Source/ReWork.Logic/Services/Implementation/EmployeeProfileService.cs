using FirstQuad.Common.Helpers;
using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System.Collections.Generic;
using System.Linq;

namespace ReWork.Logic.Services.Implementation
{
    public class EmployeeProfileService : IEmployeeProfileService
    {
        private IEmployeeProfileRepository _employeeRepository;
        private ISkillRepository _skillRepository; 
        private UserManager<User> _userManager;

        public EmployeeProfileService(IEmployeeProfileRepository employeeRep, ISkillRepository skillRep, UserManager<User> userManager)
        {
            _employeeRepository = employeeRep;
            _skillRepository = skillRep;
            _userManager = userManager;
        }


        public void CreateEmployeeProfile(string userName, int age, int[] skillsId)
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
            }  
        }

        public void EditEmployeeProfile(string employeeId, int age, int[] skillsId)
        {
            EmployeeProfile employeeProfile = _employeeRepository.FindEmployeeById(employeeId);
            if (employeeProfile != null)
            {
                employeeProfile.Age = age;
                employeeProfile.Skills.Clear();

                foreach (var id in skillsId)
                {
                    Skill skill = _skillRepository.FindById(id);
                    employeeProfile.Skills.Add(skill);
                }

                _employeeRepository.Update(employeeProfile);      
            }
        }

        public void DeleteEmployeeProfile(string employeeId)
        {
            EmployeeProfile employee = _employeeRepository.FindEmployeeById(employeeId);
            if(employee != null)
            {
                _employeeRepository.Delete(employee);
            }
        }



        public EmployeeProfileInfo FindEmployeeInfoById(string id)
        {
            return _employeeRepository.FindEmployeeInfoById(id);
        }

        public IEnumerable<EmployeeProfileInfo> FindEmployes(int[] skillsId)
        {
            var filter = PredicateBuilder.True<EmployeeProfile>();

            if (skillsId != null && skillsId.Length > 0)
            {
                filter = filter.AndAlso<EmployeeProfile>(e => e.Skills.Any(p => skillsId.Contains(p.Id)));
            }

            return _employeeRepository.FindEmployes(filter)
                                 .OrderByDescending(p => p.Rating)
                                 .ToList();
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
    }
}
