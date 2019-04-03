using Microsoft.AspNet.Identity;
using ReWork.DataProvider.Entities;
using ReWork.DataProvider.UnitOfWork;
using ReWork.Logic.Dto;
using ReWork.Logic.Services.Abstraction;
using System;
using ReWork.Common;

namespace ReWork.Logic.Services.Implementation
{
    public class EmployeeProfileService : IEmployeeProfileService
    {
        private IUnitOfWork _db;
        public EmployeeProfileService(IUnitOfWork db)
        {
            _db = db;
        }

        public void CreateEmployeeProfile(EmployeeProfileDto employee)
        {
            User user = _db.UserManager.FindByName(employee.UserName);
            if (user != null && user.EmployeeProfile == null)
            {
                EmployeeProfile employeeProfile = new EmployeeProfile() { User = user, Age = employee.Age };            
                foreach (var it in employee.Skills)
                {
                    Skill skill = _db.SkillRepository.FindSkillByTitle(it.Title);
                    employeeProfile.Skills.Add(skill);
                }

                _db.EmployeeProfileRepository.Create(employeeProfile);
                _db.SaveChanges();
            }  
        }

        public EmployeeProfileDto GetEmployeeProfileById(string id)
        {
            EmployeeProfile employeeProfile = _db.EmployeeProfileRepository.GetEmployeeProfileById(id);
            return Mapping<EmployeeProfile, EmployeeProfileDto>.MapObject(employeeProfile);
        }
    }
}
