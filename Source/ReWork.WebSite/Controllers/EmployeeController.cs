using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.Entities;
using ReWork.Model.EntitiesInfo;
using ReWork.Model.ViewModels.Account;
using ReWork.Model.ViewModels.Employee;
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
            var jobs = _jobService.FindEmployeeJobs(userId, fromDate);

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
            var employeeOffers = _offerService.FindEmployeeOffers(userId);

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
            EmployeeProfileInfo employeeProfile = _employeeService.FindEmployee(userId);

            if (employeeProfile != null)
                return View("Error");

            EmployeeProfileViewModel editModel = new EmployeeProfileViewModel() { Age = employeeProfile.Age, AboutMe = employeeProfile.AboutMe };
            editModel.SelectedSkills = employeeProfile.Skills.Select(p => p.Id).ToArray();

            ViewBag.Skills = GetCategories();
            return View(editModel);
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
            EmployeeProfileInfo employee = _employeeService.FindEmployee(id);
            if (employee == null)
                return View("Error");

            EmployeeDetailsViewModel viewModel = new EmployeeDetailsViewModel()
            {
                Id =employee.Id,
                Age = employee.Age,
                AboutMe = employee.AboutMe,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Image = employee.Image,
                UserName = employee.UserName,
                CountDevolopingJobs = employee.CountDevolopingJobs,
                RegistrationdDate = employee.RegistrationdDate,
                Skills = employee.Skills.Select(p => p.Title),
                CountReviews = employee.QualityOfWorks.Count(),
            };

            if(employee.QualityOfWorks.Count() > 0)
                viewModel.AvarageReviewMark = (int)employee.QualityOfWorks.Select(p => (int)p).Average();

            int countFeedbacksForPer = viewModel.CountReviews == 0 ? 1 : viewModel.CountReviews;
            double percentPositiveFeedBacks = (double)employee.QualityOfWorks.Count(p => (int)p >= 3) * 100 / countFeedbacksForPer;

            viewModel.PercentPositiveReviews = (int)Math.Round(percentPositiveFeedBacks);

            return View(viewModel);
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult Employees()
        {
            var sections = _sectionService.GetSectionsInfo();
            return View(sections);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Employees(int[] skillsId, string keyWords)
        {
            var employees = _employeeService.FindEmployes(skillsId, keyWords);
            return new JsonResult() { Data = employees, MaxJsonLength = Int32.MaxValue };
        }




        private IEnumerable<SelectListItem> GetCategories()
        {
            var sections = _sectionService.GetAll();
            var skills = _skillService.GetAll();

            var groups = (from se in sections
                          select new SelectListGroup() { Name = se.Title }).ToList();

            var skillsWithSections = (from se in sections
                                      join sk in skills on se.Id equals sk.SectionId
                                      select new
                                      {
                                          Id = sk.Id,
                                          Title = sk.Title,
                                          SectionTitle = se.Title
                                      }).ToList();

            var groupData = (from sk in skillsWithSections
                             select new SelectListItem()
                             {
                                 Value = sk.Id.ToString(),
                                 Text = sk.Title,
                                 Group = groups.First(sec => sec.Name.Equals(sk.SectionTitle))
                             }).ToList();

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