using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReWork.WebSite.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {   
        private IEmployeeProfileService _employeeService;
        private IUserService _userService;
        private ISectionService _sectionService;
        private ISkillService _skillService;
        private ICommitProvider _commitProvider;

        public EmployeeController(IEmployeeProfileService employeeService, IUserService userService, ISectionService sectionService, ISkillService skillService, ICommitProvider commitProvider)
        {
            _employeeService = employeeService;
            _userService = userService;
            _sectionService = sectionService;
            _skillService = skillService;
            _commitProvider = commitProvider;
        }

        [HttpGet]
        public ActionResult Create()
        {
            EmployeeProfileViewModel employeeProfileModel = new EmployeeProfileViewModel();
            employeeProfileModel.Skills = GetCategories();
            return View(employeeProfileModel);
        }


        [HttpPost]
        public ActionResult Create(EmployeeProfileViewModel employeeProfileModel)
        {
            if (!ModelState.IsValid)
            {
                employeeProfileModel.Skills = GetCategories();
                return View(employeeProfileModel);
            }

            _employeeService.CreateEmployeeProfile(User.Identity.Name, employeeProfileModel.Age, employeeProfileModel.SelectedSkills);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            EmployeeProfile employeeProfile = _employeeService.GetEmployeeProfileById(userId);

            if (employeeProfile != null)
            {
                EmployeeProfileViewModel employeeProfileModel = new EmployeeProfileViewModel() { Age = employeeProfile.Age };
                IEnumerable<int> employeeSkills = employeeProfile.Skills.Select(p=>p.Id);

                employeeProfileModel.Skills = GetCategories();
                employeeProfileModel.SelectedSkills = employeeSkills;

                return View(employeeProfileModel);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(EmployeeProfileViewModel employeeProfileModel)
        {
            if (!ModelState.IsValid)
            {
                employeeProfileModel.Skills = GetCategories();
                return View(employeeProfileModel);
            }

            string employeeId = User.Identity.GetUserId();
            _employeeService.EditEmployeeProfile(employeeId, employeeProfileModel.Age, employeeProfileModel.SelectedSkills);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public void Delete(string id)
        {
            _employeeService.DeleteEmployeeProfile(id);
            _commitProvider.SaveChanges();
        }



        private IEnumerable<SelectListItem> GetCategories()
        {
            IEnumerable<Section> sections = _sectionService.GetAll();
            IEnumerable<Skill> skills = _skillService.GetAll();

            IEnumerable<SelectListGroup> groups = sections.Select(p => new SelectListGroup() { Name = p.Title }).ToList();

            var skillsWithSections = sections.Join(skills, p => p.Id, p => p.SectionId,
                                                 (section, skill) => new
                                                 {
                                                     Id = skill.Id,
                                                     Title = skill.Title,
                                                     SectionTitle = section.Title
                                                 });

            IEnumerable<SelectListItem> groupData = skillsWithSections.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Title,
                Group = groups.First(sec => sec.Name.Equals(p.SectionTitle))
            });

            return groupData;
        }

        [HttpPost]
        public ActionResult EmployeeProfileExists()
        {
            bool exists = _employeeService.EmployeeProfileExists(User.Identity.Name);
            return Json(exists);
        }
    }
}