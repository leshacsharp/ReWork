using Microsoft.AspNet.Identity;
using ReWork.Logic.Services.Abstraction;
using ReWork.Model.Context;
using ReWork.Model.ViewModels.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
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

            return RedirectToAction("settings", "account");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            string userId = User.Identity.GetUserId();
            var employeeProfile = _employeeService.FindEmployee(userId);

            if (employeeProfile == null)
                return View("Error");

            var editModel = new EmployeeProfileViewModel() { Age = employeeProfile.Age, AboutMe = employeeProfile.AboutMe };
            editModel.SelectedSkills = employeeProfile.Skills.Select(p => p.Id).ToArray();

            ViewBag.Skills = GetCategories();
            return View(editModel);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeProfileViewModel editModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Skills = GetCategories();
                return View(editModel);
            }

            string employeeId = User.Identity.GetUserId();
            _employeeService.EditEmployeeProfile(employeeId, editModel.Age, editModel.AboutMe, editModel.SelectedSkills);
            _commitProvider.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public ActionResult Delete(string employeeId)
        {
            _employeeService.DeleteEmployeeProfile(employeeId);
            _commitProvider.SaveChanges();

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(string id)
        {
            var employee = _employeeService.FindEmployee(id);
            if (employee == null)
                return View("Error");

            var employeeModel = new EmployeeDetailsViewModel()
            {
                Id = employee.Id,
                Age = employee.Age,
                AboutMe = employee.AboutMe,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                ImagePath = Convert.ToBase64String(employee.Image),
                UserName = employee.UserName,
                CountDevolopingJobs = employee.CountDevolopingJobs,
                RegistrationdDate = employee.RegistrationdDate,
                Skills = employee.Skills.Select(p => p.Title),
                CountReviews = employee.QualityOfWorks.Count(),
            };

            if(employee.QualityOfWorks.Count() > 0)
                employeeModel.AvarageReviewMark = (int)employee.QualityOfWorks.Select(p => (int)p).Average();

            int countFeedbacksForPer = employeeModel.CountReviews == 0 ? 1 : employeeModel.CountReviews;
            double percentPositiveFeedBacks = (double)employee.QualityOfWorks.Count(p => (int)p >= 3) * 100 / countFeedbacksForPer;
            employeeModel.PercentPositiveReviews = (int)Math.Round(percentPositiveFeedBacks);

            return View(employeeModel);
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

            var employeeViewModels = from e in employees
                                     select new EmployeeInfoViewModel()
                                     {
                                         Id = e.Id,
                                         UserName = e.UserName,
                                         FirstName = e.FirstName,
                                         LastName = e.LastName,
                                         RegistrationdDate = e.RegistrationdDate,
                                         QualityOfWorks = e.QualityOfWorks,
                                         Skills = e.Skills,
                                         ImagePath = Convert.ToBase64String(e.Image)
                                     };

            return Json(employeeViewModels);
        }




        [HttpPost]
        public ActionResult ProfileExists(string userId)
        {
            bool exists = _employeeService.EmployeeProfileExists(userId);
            return Json(exists);
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
    }
}