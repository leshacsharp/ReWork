using FirstQuad.Common.Helpers;
using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Repositories.Abstraction;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
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


        public void CreateEmployeeProfile(string userId, int age, string aboutMe, int[] skillsId)
        {
            var user = _userManager.FindById(userId);
            if (user == null)
                throw new ObjectNotFoundException($"User with id={userId} not found");

            if (user.EmployeeProfile != null)
                throw new ArgumentException($"Employee profile with id={userId} already exists", "EmployeeProfile");


            var employeeProfile = new EmployeeProfile() { User = user, Age = age, AboutMe = aboutMe };

            foreach (var skillId in skillsId)
            {
                Skill skill = _skillRepository.FindById(skillId);

                if (skill == null)
                    throw new ObjectNotFoundException($"Skill with id={skillId} not found");

                employeeProfile.Skills.Add(skill);
            }

            _employeeRepository.Create(employeeProfile);
        }

        public void EditEmployeeProfile(string employeeId, int age, string aboutMe, int[] skillsId)
        {
            var employeeProfile = _employeeRepository.FindEmployeeById(employeeId);
            if (employeeProfile == null)
                throw new ObjectNotFoundException($"Employee profile with id={employeeId} not found");

            employeeProfile.Age = age;
            employeeProfile.AboutMe = aboutMe;
            employeeProfile.Skills.Clear();

            foreach (var skillId in skillsId)
            {
                Skill skill = _skillRepository.FindById(skillId);

                if (skill == null)
                    throw new ObjectNotFoundException($"Skill with id={skillId} not found");

                employeeProfile.Skills.Add(skill);
            }

            _employeeRepository.Update(employeeProfile);
        }

        public void DeleteEmployeeProfile(string employeeId)
        {
            var employee = _employeeRepository.FindEmployeeById(employeeId);
            if (employee == null)
                throw new ObjectNotFoundException($"Employee profile with id={employeeId} not found");

            employee.DevelopingJobs.Clear();

            _employeeRepository.Delete(employee); 
        }


        public EmployeeProfileInfo FindEmployee(string employeeId)
        {
            return _employeeRepository.FindEmployes(p => p.Id == employeeId).SingleOrDefault();
        }

        public IEnumerable<EmployeeProfileInfo> FindEmployes(int[] skillsId, string keyWords)
        {
            var filter = PredicateBuilder.True<EmployeeProfile>();

            if (skillsId != null && skillsId.Length > 0)
            {
                filter = filter.AndAlso<EmployeeProfile>(e => e.Skills.Any(p => skillsId.Contains(p.Id)));
            }

            if (!String.IsNullOrEmpty(keyWords))
            {
                filter = filter.AndAlso<EmployeeProfile>(e =>
                                e.AboutMe.Contains(keyWords) ||
                                e.User.FirstName.Contains(keyWords) ||
                                e.User.LastName.Contains(keyWords) ||
                                e.User.UserName.Contains(keyWords));
            }
           
            return _employeeRepository.FindEmployes(filter)
                                 .OrderByDescending(p=>p.CountDevolopingJobs)
                                 .ToList();
        }


        public bool EmployeeProfileExists(string employeeId)
        {
            var employeeProfile = _employeeRepository.FindEmployeeById(employeeId);;
            return employeeProfile != null;
        }
    }
}
