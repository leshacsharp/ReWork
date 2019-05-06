using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
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
        private IJobService _jobService;
        private IOfferService _offerService;
        private ISectionService _sectionService;
        private ISkillService _skillService;
        private ICommitProvider _commitProvider;

        public EmployeeController(IEmployeeProfileService employeeService, IJobService jobService, IOfferService offerService, IUserService userService, ISectionService sectionService, ISkillService skillService, ICommitProvider commitProvider)
        {
            _employeeService = employeeService;
            _userService = userService;
            _jobService = jobService;
            _offerService = offerService;
            _sectionService = sectionService;
            _skillService = skillService;
            _commitProvider = commitProvider;
        }


        [HttpGet]
        public ActionResult MyJobs()
        {     
            return View();
        }

        [HttpPost]
        public ActionResult MyJobs(DateTime? fromDate)
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<JobInfo> jobs = _jobService.FindEmployeeJobs(userId, fromDate);

            return Json(jobs);
        }

        [HttpGet]
        public ActionResult MyOffers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeOffers()
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<OfferInfo> employeeOffers = _offerService.FindEmployeeOffers(userId);

            return Json(employeeOffers);
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Skills = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeProfileViewModel createModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Skills = GetCategories();
                return View(createModel);
            }

            string userId = User.Identity.GetUserId();
            _employeeService.CreateEmployeeProfile(userId, createModel.Age, createModel.AboutMe, createModel.SelectedSkills);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            EmployeeProfileInfo employeeProfile = _employeeService.FindEmployeeInfoById(userId);

            if (employeeProfile != null)
            {
                EmployeeProfileViewModel editModel = new EmployeeProfileViewModel() { Age = employeeProfile.Age, AboutMe = employeeProfile.AboutMe };
                editModel.SelectedSkills = employeeProfile.Skills.Select(p => p.Id).ToArray();

                ViewBag.Skills = GetCategories();
                return View(editModel);
            }
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(EmployeeProfileViewModel editModel)
        {
            //TODO: изменить вместо form на ajax
            if (!ModelState.IsValid)
            {
                ViewBag.Skills = GetCategories();
                return View(editModel);
            }

            string employeeId = User.Identity.GetUserId();
            _employeeService.EditEmployeeProfile(employeeId, editModel.Age, editModel.AboutMe, editModel.SelectedSkills);
            _commitProvider.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public void Delete(string id)
        {
            _employeeService.DeleteEmployeeProfile(id);
            _commitProvider.SaveChanges();
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(string id)
        {
            EmployeeProfileInfo employee = _employeeService.FindEmployeeInfoById(id);

            return employee != null ? View(employee) : View("Error");
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult Employees()
        {
            IEnumerable<SectionInfo> sections = _sectionService.GetSectionsInfo();
            return View(sections);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Employees(int[] skillsId, string keyWords)
        {
            IEnumerable<EmployeeProfileInfo> employees = _employeeService.FindEmployes(skillsId, keyWords);
            return Json(employees);
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
            string userId = User.Identity.GetUserId();
            bool exists = _employeeService.EmployeeProfileExists(userId);
            return Json(exists);
        }
    }
}